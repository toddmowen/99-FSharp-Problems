// [snippet: Ninety-Nine F# Problems - Problems 1 - 10 - Lists]
/// These are F# translations of Ninety-Nine Haskell Problems 
/// (http://www.haskell.org/haskellwiki/H-99:_Ninety-Nine_Haskell_Problems), 
/// which are themselves translations of Ninety-Nine Lisp Problems
/// (http://www.ic.unicamp.br/~meidanis/courses/mc336/2006s2/funcional/L-99_Ninety-Nine_Lisp_Problems.html)
/// and Ninety-Nine Prolog Problems
/// (https://sites.google.com/site/prologsite/prolog-problems).
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

// [snippet: (*) Problem 1 : Find the last element of a list.]
/// Example in F#: 
/// > myLast [1; 2; 3; 4];;
/// val it : int = 4
/// > myLast ['x';'y';'z'];;
/// val it : char = 'z'

// A recursive definition, using the "function" keyword.
// Perhaps the shape of this code belies my Haskell origins?
let rec myLast = function
    | []    -> failwith "empty list"
    | [x]   -> x
    | _::xs -> myLast xs

// The same recursive definition, but using the "match" keyword.
// This seems to be the more common idiom in F#.
let rec myLast' xs =
    match xs with
    | []        -> failwith "empty list"
    | [x]       -> x
    | _::tail   -> myLast' tail

// [/snippet]

// [snippet: (*) Problem 2 : Find the last but one element of a list.]
/// (Note that the Lisp transcription of this problem is incorrect.) 
///
/// Example in F#: 
/// myButLast [1; 2; 3; 4];;
/// val it : int = 3
/// > myButLast ['a'..'z'];;
/// val it : char = 'y'

// I actually prefer the first of these two versions, but that may just
// be because I'm not yet used to reading F#'s index operator.
let myButLast xs = List.nth (List.rev xs) 1
let myButLast' xs = (List.rev xs).[1]

// A version using fold.
let myButLast'' (x::y::zs) =
    List.fold(fun (x,y) z -> (y,z)) (x,y) zs
    |> fst

// "Array-like" syntax: clear, but doesn't teach you anything about lists!
let myButLast''' (xs : 'a list) = xs.[xs.Length - 2]

// [/snippet]

// [snippet: (*) Problem 3 : Find the K'th element of a list. The first element in the list is number 1.]
/// Example: 
/// * (element-at '(a b c d e) 3)
/// c
/// 
/// Example in F#: 
/// > elementAt [1; 2; 3] 2;;
/// val it : int = 2
/// > elementAt (List.ofSeq "fsharp") 5;;
/// val it : char = 'r'

// Vaguely inspired by the Haskell "zip with an infinite list" idiom,
// except that we get indexes from Seq.mapi instead of an infinite list.
let elementAt xs n =
    xs
    |> Seq.mapi (fun zeroBasedIndex x -> (zeroBasedIndex+1,x))
    |> Seq.filter (fun (oneBasedIndex,x) -> oneBasedIndex = n)
    |> Seq.head |> snd

let elementAt' xs n = xs |> Seq.skip (n-1) |> Seq.head

// The "zip with an infinite list" approach does work after all.
let elementAt'' xs n =
    seq { for (i,x) in Seq.zip {1..System.Int32.MaxValue} xs do if i = n then yield x }
    |> Seq.head

// [/snippet]

// [snippet: (*) Problem 4 : Find the number of elements of a list.]
/// Example in F#: 
/// 
/// > myLength [123; 456; 789];;
/// val it : int = 3
/// > myLength <| List.ofSeq "Hello, world!"
/// val it : int = 13 

// Let's try using local state.
let myLength xs =
    let count = ref 0
    xs |> List.iter (fun _ -> count := !count + 1)
    !count

// Now back to the safety of recursion (encapsulated in a fold)...
let myLength' xs = xs |> List.fold (fun n _ -> n + 1) 0

// [/snippet]

// [snippet: (*) Problem 5 : Reverse a list.]
/// Example in F#: 
///
/// > reverse <| List.ofSeq ("A man, a plan, a canal, panama!")
/// val it : char list =
///  ['!'; 'a'; 'm'; 'a'; 'n'; 'a'; 'p'; ' '; ','; 'l'; 'a'; 'n'; 'a'; 'c'; ' ';
///   'a'; ' '; ','; 'n'; 'a'; 'l'; 'p'; ' '; 'a'; ' '; ','; 'n'; 'a'; 'm'; ' ';
///   'A']
/// > reverse [1,2,3,4];;
/// val it : int list = [4; 3; 2; 1]

let reverse lst =
    let rec loop acc lst =
        match lst with
        | []    -> acc
        | x::xs -> loop (x::acc) xs
    loop [] lst

let reverse' lst = lst |> List.fold (fun acc x -> x::acc) []

// [/snippet]

// [snippet: (*) Problem 6 : Find out whether a list is a palindrome.]
/// A palindrome can be read forward or backward; e.g. (x a m a x).
/// 
/// Example in F#: 
/// > isPalindrome [1;2;3];;
/// val it : bool = false
/// > isPalindrome <| List.ofSeq "madamimadam";;
/// val it : bool = true
/// > isPalindrome [1;2;4;8;16;8;4;2;1];;
/// val it : bool = true

let isPalindrome lst = (reverse lst) = lst

let isPalindrome' lst =
    let arr = Array.ofSeq lst
    let middle = arr.Length / 2
    Seq.forall (fun i -> arr.[i] = arr.[arr.Length - i - 1]) {0..middle}

// [/snippet]

// [snippet: (**) Problem 7 : Flatten a nested list structure.]
/// Transform a list, possibly holding lists as elements into a `flat' list by replacing each 
/// list with its elements (recursively).
///  
/// Example: 
/// * (my-flatten '(a (b (c d) e)))
/// (A B C D E)
///  
/// Example in F#: 
/// 
type 'a NestedList = List of 'a NestedList list | Elem of 'a
///
/// > flatten (Elem 5);;
/// val it : int list = [5]
/// > flatten (List [Elem 1; List [Elem 2; List [Elem 3; Elem 4]; Elem 5]]);;
/// val it : int list = [1;2;3;4;5]
/// > flatten (List [] : int List);;
/// val it : int list = []


// [/snippet]

// [snippet: (**) Problem 8 : Eliminate consecutive duplicates of list elements.] 
/// If a list contains repeated elements they should be replaced with a single copy of the 
/// element. The order of the elements should not be changed.
///  
/// Example: 
/// * (compress '(a a a a b c c a a d e e e e))
/// (A B C A D E)
///  
/// Example in F#: 
/// 
/// > compress ["a";"a";"a";"a";"b";"c";"c";"a";"a";"d";"e";"e";"e";"e"];;
/// val it : string list = ["a";"b";"c";"a";"d";"e"]


// [/snippet]

// [snippet: (**) Problem 9 : Pack consecutive duplicates of list elements into sublists.] 
/// If a list contains repeated elements they should be placed 
/// in separate sublists.
///  
/// Example: 
/// * (pack '(a a a a b c c a a d e e e e))
/// ((A A A A) (B) (C C) (A A) (D) (E E E E))
///  
/// Example in F#: 
/// 
/// > pack ['a'; 'a'; 'a'; 'a'; 'b'; 'c'; 'c'; 'a'; 
///         'a'; 'd'; 'e'; 'e'; 'e'; 'e']
/// val it : char list list =
///  [['a'; 'a'; 'a'; 'a']; ['b']; ['c'; 'c']; ['a'; 'a']; ['d'];
///   ['e'; 'e'; 'e'; 'e']]

// [/snippet]

// [snippet: (*) Problem 10 : Run-length encoding of a list.]
/// Use the result of problem P09 to implement the so-called run-length 
/// encoding data compression method. Consecutive duplicates of elements 
/// are encoded as lists (N E) where N is the number of duplicates of the element E.
///  
/// Example: 
/// * (encode '(a a a a b c c a a d e e e e))
/// ((4 A) (1 B) (2 C) (2 A) (1 D)(4 E))
///  
/// Example in F#: 
/// 
/// encode <| List.ofSeq "aaaabccaadeeee"
/// val it : (int * char) list =
///   [(4,'a');(1,'b');(2,'c');(2,'a');(1,'d');(4,'e')]



// [/snippet]
