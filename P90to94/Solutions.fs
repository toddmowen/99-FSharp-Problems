// [snippet: Ninety-Nine F# Problems - Problems 90 - 94 - Miscellaneous problems]
/// Ninety-Nine F# Problems - Problems 90 - 94
///
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
///
// [/snippet]

// [snippet: (**) Problem 90 : Eight queens problem]
/// This is a classical problem in computer science. The objective is to place eight queens on a 
/// chessboard so that no two queens are attacking each other; i.e., no two queens are in the 
/// same row, the same column, or on the same diagonal.
///  
/// Hint: Represent the positions of the queens as a list of numbers 1..N. Example: 
/// [4,2,7,3,6,8,5,1] means that the queen in the first column is in row 4, the queen in the 
/// second column is in row 2, etc. Use the generate-and-test paradigm.
///  
/// Example in F#: 
/// 
/// > queens 8 |> Seq.length;;
/// val it : int = 92
/// > queens 8 |> Seq.head;;
/// val it : int list = [1; 5; 8; 6; 3; 7; 2; 4]
/// > queens 20 |> Seq.head;;
/// val it : int list =
///  [1; 3; 5; 2; 4; 13; 15; 12; 18; 20; 17; 9; 16; 19; 8; 10; 7; 14; 6; 11]

// [/snippet]

// [snippet: (**) Problem 91 : Knight's tour]
/// Another famous problem is this one: How can a knight jump on an NxN chessboard in such a way 
/// that it visits every square exactly once? A set of solutions is given on the The_Knights_Tour
/// page.
///  
/// Hints: Represent the squares by pairs of their coordinates of the form X/Y, where both X and 
/// Y are integers between 1 and N. (Note that '/' is just a convenient functor, not division!) 
/// Define the relation jump(N,X/Y,U/V) to express the fact that a knight can jump from X/Y to U/V 
/// on a NxN chessboard. And finally, represent the solution of our problem as a list of N*N knight
/// positions (the knight's tour).
///  
/// There are two variants of this problem: 
/// 1. find a tour ending at a particular square 
/// 2. find a circular tour, ending a knight's jump from the start (clearly it doesn't matter where 
///    you start, so choose (1,1))
///  
/// Example in F#: 
/// 
/// > knightsTour 8 (1,1) |> Seq.head;;
/// val it : (int * int) list =
///   [(4, 3); (6, 4); (5, 6); (4, 8); (3, 6); (5, 5); (6, 3); (4, 4); (2, 3);
///    (1, 5); (3, 4); (5, 3); (6, 5); (4, 6); (2, 7); (3, 5); (5, 4); (6, 6);
///    (4, 5); (2, 4); (1, 6); (2, 8); (4, 7); (6, 8); (8, 7); (7, 5); (8, 3);
///    (7, 1); (5, 2); (3, 1); (1, 2); (3, 3); (4, 1); (2, 2); (1, 4); (2, 6);
///    (1, 8); (3, 7); (5, 8); (7, 7); (8, 5); (7, 3); (8, 1); (6, 2); (7, 4);
///    (8, 2); (6, 1); (4, 2); (2, 1); (1, 3); (2, 5); (1, 7); (3, 8); (5, 7);
///    (7, 8); (8, 6); (6, 7); (8, 8); (7, 6); (8, 4); (7, 2); (5, 1); (3, 2);
///    (1, 1)]
///
/// > endKnightsTour 8 (4,2);;
/// val it : (int * int) list =
///   [(4, 2); (2, 1); (1, 3); (3, 2); (1, 1); (2, 3); (1, 5); (2, 7); (4, 8);
///    (6, 7); (8, 8); (7, 6); (6, 8); (8, 7); (7, 5); (8, 3); (7, 1); (5, 2);
///    (3, 1); (1, 2); (2, 4); (1, 6); (2, 8); (4, 7); (2, 6); (1, 8); (3, 7);
///    (5, 8); (7, 7); (8, 5); (7, 3); (8, 1); (6, 2); (4, 1); (2, 2); (1, 4);
///    (3, 5); (5, 6); (4, 4); (2, 5); (1, 7); (3, 8); (5, 7); (7, 8); (8, 6);
///    (7, 4); (6, 6); (4, 5); (3, 3); (5, 4); (4, 6); (6, 5); (8, 4); (7, 2);
///    (6, 4); (4, 3); (5, 1); (6, 3); (8, 2); (6, 1); (5, 3); (3, 4); (5, 5);
///    (3, 6)]
///
/// > closedKnightsTour 8;;
/// val it : (int * int) list =
///   [(2, 3); (4, 4); (6, 3); (5, 5); (4, 3); (6, 4); (5, 6); (4, 8); (3, 6);
///    (1, 5); (3, 4); (5, 3); (6, 5); (4, 6); (2, 7); (3, 5); (5, 4); (6, 6);
///    (4, 5); (2, 4); (1, 6); (2, 8); (4, 7); (6, 8); (8, 7); (7, 5); (8, 3);
///    (7, 1); (5, 2); (3, 1); (1, 2); (3, 3); (4, 1); (2, 2); (1, 4); (2, 6);
///    (1, 8); (3, 7); (5, 8); (7, 7); (8, 5); (7, 3); (8, 1); (6, 2); (7, 4);
///    (8, 2); (6, 1); (4, 2); (2, 1); (1, 3); (2, 5); (1, 7); (3, 8); (5, 7);
///    (7, 8); (8, 6); (6, 7); (8, 8); (7, 6); (8, 4); (7, 2); (5, 1); (3, 2);
///    (1, 1)]

// [/snippet]

// [snippet: (***) Problem 92 : Von Koch's conjecture]
/// Several years ago I met a mathematician who was intrigued by a problem for which he didn't
/// know a solution. His name was Von Koch, and I don't know whether the problem has been 
/// solved since.
///  
///                                         6
///        (d)   (e)---(f)        (4)   (1)---(7)
///         |     |              1 |     | 5
///        (a)---(b)---(c)        (3)---(6)---(2)
///         |                    2 |  3     4
///        (g)                    (5)
///
/// Anyway the puzzle goes like this: Given a tree with N nodes (and hence N-1 edges). Find a 
/// way to enumerate the nodes from 1 to N and, accordingly, the edges from 1 to N-1 in such 
/// a way, that for each edge K the difference of its node numbers equals to K. The conjecture 
/// is that this is always possible.
///  
/// For small trees the problem is easy to solve by hand. However, for larger trees, and 14 is 
/// already very large, it is extremely difficult to find a solution. And remember, we don't 
/// know for sure whether there is always a solution!
///  
/// Write a predicate that calculates a numbering scheme for a given tree. What is the solution
/// for the larger tree pictured below?
///
///     (i) (g)   (d)---(k)         (p)
///        \ |     |                 |
///         (a)---(c)---(e)---(q)---(n)
///        / |     |           |
///     (h) (b)   (f)         (m)
///
/// Example in F#:  
/// > vonKoch (['d';'a';'g';'b';'c';'e';'f'],[('d', 'a');('a', 'g');('a', 'b');('b', 'e');
///                ('b', 'c');('e', 'f')]) |> Seq.head;;
///
/// val it : int list * (int * int * int) list =
///   ([4; 3; 5; 6; 2; 1; 7],
///    [(4, 3, 1); (3, 5, 2); (3, 6, 3); (6, 1, 5); (6, 2, 4); (1, 7, 6)])
///

// [/snippet]


// [snippet: (***) Problem 93 : An arithmetic puzzle]
/// Given a list of integer numbers, find a correct way of inserting arithmetic signs (operators)
/// such that the result is a correct equation. Example: With the list of numbers [2,3,5,7,11] we 
/// can form the equations 2-3+5+7 = 11 or 2 = (3*5+7)/11 (and ten others!).
///  
/// Division should be interpreted as operating on rationals, and division by zero should be 
/// avoided.
///  
/// Example in F#: 
/// 
/// > solutions [2;3;5;7;11] |> List.iter (printfn "%s");;
/// 2 = 3 - (5 + (7 - 11))
/// 2 = 3 - ((5 + 7) - 11)
/// 2 = (3 - 5) - (7 - 11)
/// 2 = (3 - (5 + 7)) + 11
/// 2 = ((3 - 5) - 7) + 11
/// 2 = ((3 * 5) + 7) / 11
/// 2 * (3 - 5) = 7 - 11
/// 2 - (3 - (5 + 7)) = 11
/// 2 - ((3 - 5) - 7) = 11
/// (2 - 3) + (5 + 7) = 11
/// (2 - (3 - 5)) + 7 = 11
/// ((2 - 3) + 5) + 7 = 11
/// val it : unit = ()
///

// [/snippet]

// [snippet: (***) Problem 94 : Generate K-regular simple graphs with N nodes]
/// In a K-regular graph all nodes have a degree of K; i.e. the number of edges incident in each 
/// node is K. How many (non-isomorphic!) 3-regular graphs with 6 nodes are there?

// [/snippet]
