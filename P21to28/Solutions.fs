﻿// [snippet: Ninety-Nine F# Problems - Problems 21 - 28 - Lists again]
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

// [snippet: (*) Problem 21 : Insert an element at a given position into a list.]
/// Example: 
/// * (insert-at 'alfa '(a b c d) 2)
/// (A ALFA B C D)
///  
/// Example in F#: 
/// 
/// > insertAt 'X' (List.ofSeq "abcd") 2;;
/// val it : char list = ['a'; 'X'; 'b'; 'c'; 'd']


// [/snippet]

// [snippet: (*) Problem 22 : Create a list containing all integers within a given range.]
/// Example: 
/// * (range 4 9)
/// (4 5 6 7 8 9)
///  
/// Example in F#: 
/// 
/// > range 4 9;;
/// val it : int list = [4; 5; 6; 7; 8; 9]

// [/snippet]

// [snippet: (**) Problem 23 : Extract a given number of randomly selected elements from a list.]
/// Example: 
/// * (rnd-select '(a b c d e f g h) 3)
/// (E D A)
///  
/// Example in F#: 
/// 
/// > rnd_select "abcdefgh" 3;;
/// val it : seq<char> = seq ['e'; 'a'; 'h']

// [/snippet]

// [snippet: (*) Problem 24 : Lotto: Draw N different random numbers from the set 1..M.]
/// Example: 
/// * (rnd-select 6 49)
/// (23 1 17 33 21 37)
///  
/// Example in F#: 
/// 
/// > diff_select 6 49;;
/// val it : int list = [27; 20; 22; 9; 15; 29]

// using problem 23

// [/snippet]

// [snippet: (*) Problem 25 : Generate a random permutation of the elements of a list.]
/// Example: 
/// * (rnd-permu '(a b c d e f))
/// (B A D C E F)
///  
/// Example in F#: 
/// 
/// > rnd_permu <| List.ofSeq "abcdef";;
/// val it : char list = ['b'; 'c'; 'd'; 'f'; 'e'; 'a']


// [/snippet]

// [snippet: (**) Problem 26 : Generate the combinations of K distinct objects chosen from the N elements of a list.]
/// In how many ways can a committee of 3 be chosen from a group of 12 people? We all know that 
/// there are C(12,3) = 220 possibilities (C(N,K) denotes the well-known binomial coefficients). For 
/// pure mathematicians, this result may be great. But we want to really generate all the 
/// possibilities in a list.
///  
/// Example: 
/// * (combinations 3 '(a b c d e f))
/// ((A B C) (A B D) (A B E) ... )
///  
/// Example in F#: 
/// 
/// > combinations 3 ['a' .. 'f'];;
/// val it : char list list =
///   [['a'; 'b'; 'c']; ['a'; 'b'; 'd']; ['a'; 'b'; 'e']; ['a'; 'b'; 'f'];
///    ['a'; 'c'; 'd']; ['a'; 'c'; 'e']; ['a'; 'c'; 'f']; ['a'; 'd'; 'e'];
///    ['a'; 'd'; 'f']; ['a'; 'e'; 'f']; ['b'; 'c'; 'd']; ['b'; 'c'; 'e'];
///    ['b'; 'c'; 'f']; ['b'; 'd'; 'e']; ['b'; 'd'; 'f']; ['b'; 'e'; 'f'];
///    ['c'; 'd'; 'e']; ['c'; 'd'; 'f']; ['c'; 'e'; 'f']; ['d'; 'e'; 'f']] 


// [/snippet]

// [snippet: (**) Problem 27 : Group the elements of a set into disjoint subsets.] 
/// a) In how many ways can a group of 9 people work in 3 disjoint subgroups of 2, 3 
/// and 4 persons? Write a function that generates all the possibilities and returns them 
/// in a list.
///  
/// Example: 
/// * (group3 '(aldo beat carla david evi flip gary hugo ida))
/// ( ( (ALDO BEAT) (CARLA DAVID EVI) (FLIP GARY HUGO IDA) )
/// ... )
///  
/// b) Generalize the above predicate in a way that we can specify a list of group sizes 
/// and the predicate will return a list of groups.
///  
/// Example: 
/// * (group '(aldo beat carla david evi flip gary hugo ida) '(2 2 5))
/// ( ( (ALDO BEAT) (CARLA DAVID) (EVI FLIP GARY HUGO IDA) )
/// ... )
///  
/// Note that we do not want permutations of the group members; i.e. ((ALDO BEAT) ...) 
/// is the same solution as ((BEAT ALDO) ...). However, we make a difference between 
/// ((ALDO BEAT) (CARLA DAVID) ...) and ((CARLA DAVID) (ALDO BEAT) ...).
///  
/// You may find more about this combinatorial problem in a good book on discrete 
/// mathematics under the term "multinomial coefficients".
///  
/// Example in F#: 
/// 
/// > group [2;3;4] ["aldo";"beat";"carla";"david";"evi";"flip";"gary";"hugo";"ida"];;
/// val it : string list list list =
///   [[["aldo"; "beat"]; ["carla"; "david"; "evi"];
///     ["flip"; "gary"; "hugo"; "ida"]];...]
/// (altogether 1260 solutions)
///  
/// > group [2;2;5] ["aldo";"beat";"carla";"david";"evi";"flip";"gary";"hugo";"ida"];;
/// val it : string list list list =
///   [[["aldo"; "beat"]; ["carla"; "david"];
///     ["evi"; "flip"; "gary"; "hugo"; "ida"]];...]
/// (altogether 756 solutions)

// [/snippet]
    
// [snippet: (**) Problem 28 : Sorting a list of lists according to length of sublists]
/// a) We suppose that a list contains elements that are lists themselves. The objective 
/// is to sort the elements of this list according to their length. E.g. short lists first,
/// longer lists later, or vice versa.
///  
/// Example: 
/// * (lsort '((a b c) (d e) (f g h) (d e) (i j k l) (m n) (o)))
/// ((O) (D E) (D E) (M N) (A B C) (F G H) (I J K L))
///  
/// Example in F#: 
/// 
/// > lsort ["abc";"de";"fgh";"de";"ijkl";"mn";"o"];;
/// val it : string list = ["o"; "de"; "de"; "mn"; "abc"; "fgh"; "ijkl"]
///
/// b) Again; we suppose that a list contains elements that are lists themselves. But this 
/// time the objective is to sort the elements of this list according to their length 
/// frequency; i.e.; in the default; where sorting is done ascendingly; lists with rare 
/// lengths are placed first; others with a more frequent length come later.
///  
/// Example: 
/// * (lfsort '((a b c) (d e) (f g h) (d e) (i j k l) (m n) (o)))
/// ((i j k l) (o) (a b c) (f g h) (d e) (d e) (m n))
///  
/// Example in F#: 
/// 
/// > lfsort ["abc"; "de"; "fgh"; "de"; "ijkl"; "mn"; "o"];;
/// val it : string list = ["ijkl"; "o"; "abc"; "fgh"; "de"; "de"; "mn"]

// [/snippet]
