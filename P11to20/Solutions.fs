// [snippet: Ninety-Nine F# Problems - Problems 11 - 20 - List, continued]
/// These are F# solutions of Ninety-Nine Haskell Problems 
/// (http://www.haskell.org/haskellwiki/H-99:_Ninety-Nine_Haskell_Problems), 
/// which are themselves translations of Ninety-Nine Lisp Problems
/// (http://www.ic.unicamp.br/~meidanis/courses/mc336/2006s2/funcional/L-99_Ninety-Nine_Lisp_Problems.html)
/// and Ninety-Nine Prolog Problems
/// (https://sites.google.com/site/prologsite/prolog-problems).
///
/// If you would like to contribute a solution or fix any bugs, send 
/// an email to paks at kitiara dot org with the subject "99 F# problems". 
/// I'll try to update the problem as soon as possible.
///
/// The problems have different levels of difficulty. Those marked with a single asterisk (*) 
/// are easy. If you have successfully solved the preceeding problems you should be able to 
/// solve them within a few (say 15) minutes. Problems marked with two asterisks (**) are of 
/// intermediate difficulty. If you are a skilled F# programmer it shouldn't take you more than 
/// 30-90 minutes to solve them. Problems marked with three asterisks (***) are more difficult. 
/// You may need more time (i.e. a few hours or more) to find a good solution
///
/// Though the problems number from 1 to 99, there are some gaps and some additions marked with 
/// letters. There are actually only 88 problems.
// [/snippet]

// [snippet: (*) Problem 11 :  Modified run-length encoding.]
/// Modify the result of problem 10 in such a way that if an element has no duplicates it 
/// is simply copied into the result list. Only elements with duplicates are transferred as
/// (N E) lists.
///  
/// Example: 
/// * (encode-modified '(a a a a b c c a a d e e e e))
/// ((4 A) B (2 C) (2 A) D (4 E))
///  
/// Example in F#: 
/// 
/// > encodeModified <| List.ofSeq "aaaabccaadeeee"
/// val it : char Encoding list =
///   [Multiple (4,'a'); Single 'b'; Multiple (2,'c'); Multiple (2,'a');
///    Single 'd'; Multiple (4,'e')]

type 'a Encoding = Multiple of int * 'a | Single of 'a

// [/snippet]

open P01to10

let encodeModified xs =
    encode xs
    |> List.map (fun (n,x) -> if n = 1 then Single x else Multiple (n,x))

// [snippet: (**) Problem 12 : Decode a run-length encoded list.]
/// Given a run-length code list generated as specified in problem 11. Construct its 
/// uncompressed version.
///  
/// Example in F#: 
/// 
/// > decodeModified 
///     [Multiple (4,'a');Single 'b';Multiple (2,'c');
///      Multiple (2,'a');Single 'd';Multiple (4,'e')];;
/// val it : char list =
///   ['a'; 'a'; 'a'; 'a'; 'b'; 'c'; 'c'; 'a'; 'a'; 'd'; 'e'; 'e'; 'e'; 'e']

// [/snippet]

let decodeModified encoding =
    [ for item in encoding do
        match item with
        | Single x          -> yield x
        | Multiple (n,x)    -> yield! List.replicate n x
    ]

// The OOP way would probably be to implement Decode as a method.
type 'a Encoding with
    member enc.Decode() =
        match enc with
        | Single x -> [x]
        | Multiple (n,x) -> List.replicate n x

// Unfortunately, the result is verbose, and cripples type inference.
let decodeModified' (encoding : 'T Encoding list) =
    encoding
    |> List.collect (fun enc -> enc.Decode())

// [snippet: (**) Problem 13 : Run-length encoding of a list (direct solution).]
/// Implement the so-called run-length encoding data compression method directly. I.e. 
/// don't explicitly create the sublists containing the duplicates, as in problem 9, 
/// but only count them. As in problem P11, simplify the result list by replacing the 
/// singleton lists (1 X) by X.
///  
/// Example: 
/// * (encode-direct '(a a a a b c c a a d e e e e))
/// ((4 A) B (2 C) (2 A) D (4 E))
///  
/// Example in F#: 
/// 
/// > encodeDirect <| List.ofSeq "aaaabccaadeeee"
/// val it : char Encoding list =
///   [Multiple (4,'a'); Single 'b'; Multiple (2,'c'); Multiple (2,'a');
///    Single 'd'; Multiple (4,'e')]

let encodeDirect lst =
    let encode x acc =
        match acc with
        | Single(y)::tail      when x = y -> Multiple(2, y)::tail
        | Multiple(n, y)::tail when x = y -> Multiple(n+1, y)::tail
        | _                               -> Single(x)::acc
    List.foldBack encode lst []

// [/snippet]

// [snippet: (*) Problem 14 : Duplicate the elements of a list.]
/// Example: 
/// * (dupli '(a b c c d))
/// (A A B B C C C C D D)
///  
/// Example in F#: 
/// 
/// > dupli [1; 2; 3]
/// [1;1;2;2;3;3]

let dupli xs = [for x in xs do yield! [x;x]]

let dupli' xs = xs |> List.collect (fun x -> [x;x])

let dupli'' lst =
    let popDouble = function
        | x::xs -> Some ([x;x], xs)
        | []    -> None
    Seq.unfold popDouble lst |> List.concat

// With so many steps, this is like imperative programming abstracted over lists :)
let dupli''' xs =
    xs
    |> List.map (Seq.singleton >> Seq.toList)  // make singleton lists: [x]
    |> List.zip xs                             // make tuples: (x, [x])
    |> List.map List.Cons                      // make two-element lists
    |> List.concat

// [/snippet]

// [snippet: (**) Problem 15 : Replicate the elements of a list a given number of times.]
/// Example: 
/// * (repli '(a b c) 3)
/// (A A A B B B C C C)
///  
/// Example in F#: 
/// 
/// > repli (List.ofSeq "abc") 3
/// val it : char list = ['a'; 'a'; 'a'; 'b'; 'b'; 'b'; 'c'; 'c'; 'c']

let repli lst n = lst |> List.collect (List.replicate n)

// Using arrays internally
let repli' lst n =
    let input = List.toArray lst
    let output = Array.init (input.Length * n) (fun i -> input.[i/n])
    Array.toList output

// [/snippet]

// [snippet: (**) Problem 16 : Drop every N'th element from a list.]
/// Example: 
/// * (drop '(a b c d e f g h i k) 3)
/// (A B D E G H K)
///  
/// Example in F#: 
/// 
/// > dropEvery (List.ofSeq "abcdefghik") 3;;
/// val it : char list = ['a'; 'b'; 'd'; 'e'; 'g'; 'h'; 'k']

// Write a filteri function, following the pattern of List.mapi and List.iteri.
// At first I tried to do this using options, but this turned out to be painful
// because neither List.collect or List.concat can accept a list of options, and I
// can't find any core functions that do (e.g. equivalent to Haskell's catMaybes).
let filteri test xs =
    xs
    |> List.mapi (fun i x -> if (test i x) then Some(x) else None)
    |> List.filter Option.isSome
    |> List.map Option.get

// Using lists as a poor man's option type gives us a more concise implementation:
let filteri' test xs =
    xs
    |> List.mapi (fun i x -> [if test i x then yield x])
    |> List.concat

// Alternatively, I like list comprehensions, but in this case they don't gain us much.
let filteri'' test xs =
    let ixs = List.mapi (fun i x -> (i,x)) xs
    [for (i,x) in ixs do if test i x then yield x]

let dropEvery lst n = lst |> filteri'' (fun i _ -> (i+1) % n <> 0)

// [/snippet]

// [snippet: (*) Problem 17 : Split a list into two parts; the length of the first part is given.]
/// Do not use any predefined predicates. 
/// 
/// Example: 
/// * (split '(a b c d e f g h i k) 3)
/// ( (A B C) (D E F G H I K))
///  
/// Example in F#: 
/// 
/// > split (List.ofSeq "abcdefghik") 3
/// val it : char list * char list =
///   (['a'; 'b'; 'c'], ['d'; 'e'; 'f'; 'g'; 'h'; 'i'; 'k'])

// Continuation passing style.
let split lst n =
    let rec sp lst n cont =
        if n = 0 then cont ([], lst)
        else match lst with
                | []    -> failwith "List too short"
                | x::xs -> sp xs (n-1) (fun (front, back) -> cont (x::front, back))
    sp lst n id

// [/snippet]

// [snippet: (**) Problem 18 : Extract a slice from a list.]
/// Given two indices, i and k, the slice is the list containing the elements between the 
/// i'th and k'th element of the original list (both limits included). Start counting the 
/// elements with 1.
///  
/// Example: 
/// * (slice '(a b c d e f g h i k) 3 7)
/// (C D E F G)
///  
/// Example in F#: 
/// 
/// > slice ['a';'b';'c';'d';'e';'f';'g';'h';'i';'k'] 3 7;;
/// val it : char list = ['c'; 'd'; 'e'; 'f'; 'g']

let slice lst j k =
    let skipLen = j - 1
    let sliceLen = k - j + 1
    lst |> Seq.skip skipLen |> Seq.take sliceLen |> Seq.toList

let slice' lst j k = filteri (fun i _ -> j <= (i+1) && (i+1) <= k) lst 

// [/snippet]

// [snippet: (**) Problem 19 : Rotate a list N places to the left.]
/// Hint: Use the predefined functions length and (@) 
/// 
/// Examples: 
/// * (rotate '(a b c d e f g h) 3)
/// (D E F G H A B C)
/// 
/// * (rotate '(a b c d e f g h) -2)
/// (G H A B C D E F)
///  
/// Examples in F#: 
/// 
/// > rotate ['a';'b';'c';'d';'e';'f';'g';'h'] 3;;
/// val it : char list = ['d'; 'e'; 'f'; 'g'; 'h'; 'a'; 'b'; 'c']
///  
/// > rotate ['a';'b';'c';'d';'e';'f';'g';'h'] (-2);;
/// val it : char list = ['g'; 'h'; 'a'; 'b'; 'c'; 'd'; 'e'; 'f']

let modulo x n = (x % n + n) % n

let rotate lst n =
    let rightShift = modulo n (List.length lst)
    Seq.skip rightShift lst
    |> Seq.append <| Seq.take rightShift lst
    |> Seq.toList

let rotate' lst n =
    let len = List.length lst
    lst @ lst
    |> Seq.skip (modulo n len)
    |> Seq.take len
    |> Seq.toList

// [/snippet]

// [snippet: (*) Problem 20 : Remove the K'th element from a list.]
/// Example in Prolog: 
/// ?- remove_at(X,[a,b,c,d],2,R).
/// X = b
/// R = [a,c,d]
///  
/// Example in Lisp: 
/// * (remove-at '(a b c d) 2)
/// (A C D)
///  
/// (Note that this only returns the residue list, while the Prolog version also returns 
/// the deleted element.)
///  
/// Example in F#: 
/// 
/// > removeAt 1 <| List.ofSeq "abcd";;
/// val it : char * char list = ('b', ['a'; 'c'; 'd'])

let removeAt n lst =
    match split lst n with
    | (front, del::back) -> (del, front @ back)
    | _                  -> failwith "Index out of bounds"

// [/snippet]
