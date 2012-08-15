// [snippet: Ninety-Nine F# Problems - Problems 80 - 89 - Graphs]
/// Ninety-Nine F# Problems - Problems 80 - 89
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

// The solutions to the problems below use there definitions for Grahps
type 'a Edge = 'a * 'a

type 'a Graph = 'a list * 'a Edge list

let g = (['b';'c';'d';'f';'g';'h';'k'],[('b','c');('b','f');('c','f');('f','k');('g','h')])

type 'a Node = 'a * 'a list

type 'a AdjacencyGraph = 'a Node list

let ga = [('b',['c'; 'f']); ('c',['b'; 'f']); ('d',[]); ('f',['b'; 'c'; 'k']); 
                                                    ('g',['h']); ('h',['g']); ('k',['f'])]

// [/snippet]

// [snippet: (***) Problem 80 : Conversions]
/// Write predicates to convert between the different graph representations. With these 
/// predicates, all representations are equivalent; i.e. for the following problems you 
/// can always pick freely the most convenient form. The reason this problem is rated 
/// (***) is not because it's particularly difficult, but because it's a lot of work to 
/// deal with all the special cases. 
/// 
/// Example in F#:
/// 
/// > let g = (['b';'c';'d';'f';'g';'h';'k'],[('b','c');('b','f');
///                                                 ('c','f');('f','k');('g','h')]);;
/// 
/// > graph2AdjacencyGraph g;;
/// val it : char AdjacencyGraph =
///   [('b', ['f'; 'c']); ('c', ['f'; 'b']); ('d', []); ('f', ['k'; 'c'; 'b']);
///    ('g', ['h']); ('h', ['g']); ('k', ['f'])]
///
/// > let ga = [('b',['c'; 'f']); ('c',['b'; 'f']); ('d',[]); ('f',['b'; 'c'; 'k']); 
///                                             ('g',['h']); ('h',['g']); ('k',['f'])];;
/// 
/// > adjacencyGraph2Graph ga;;
/// val it : char Graph =
///   (['b'; 'c'; 'd'; 'f'; 'g'; 'h'; 'k'],
///    [('b', 'c'); ('b', 'f'); ('c', 'f'); ('f', 'k'); ('g', 'h')])

// [/snippet]

// [snippet: (**) Problem 81: Path from one node to another one]
/// Write a function that, given two nodes a and b in a graph, returns all 
/// the acyclic paths from a to b.
/// 
/// Example:
/// 
/// Example in F#:
/// 
/// > paths 1 4 [(1,[2;3]);(2,[3]);(3,[4]);(4,[2]);(5,[6]);(6,[5])];;
/// val it : int list list = [[1; 2; 3; 4]; [1; 3; 4]]
///
/// > paths 2 6 [(1,[2;3]);(2,[3]);(3,[4]);(4,[2]);(5,[6]);(6,[5])];;
/// val it : int list list = []

// [/snippet]


// [snippet: (*) Problem 82: Cycle from a given node]
/// Write a predicate cycle(G,A,P) to find a closed path (cycle) P starting at a given node
///  A in the graph G. The predicate should return all cycles via backtracking.
/// 
/// Example:
/// 
/// <example in lisp>
/// Example in F#:
/// 
/// > cycle 2 [(1,[2;3]);(2,[3]);(3,[4]);(4,[2]);(5,[6]);(6,[5])];;
/// val it : int list list = [[2; 3; 4; 2]]
///
/// > cycle 1 [(1,[2;3]);(2,[3]);(3,[4]);(4,[2]);(5,[6]);(6,[5])];;
/// val it : int list list = []

// [/snippet]

// [snippet: (**) Problem 83: Construct all spanning trees]
/// Write a predicate s_tree(Graph,Tree) to construct (by backtracking) all spanning trees 
/// of a given graph. With this predicate, find out how many spanning trees there are for 
/// the graph depicted to the left. The data of this example graph can be found in the file
/// p83.dat. When you have a correct solution for the s_tree/2 predicate, use it to define 
/// two other useful predicates: is_tree(Graph) and is_connected(Graph). Both are 
/// five-minutes tasks!
/// 
/// Example:
/// 
/// <example in lisp>
/// Example in F#:

// [/snippet]

// [snippet: (**) Problem 84: Construct the minimal spanning tree]
/// Write a predicate ms_tree(Graph,Tree,Sum) to construct the minimal spanning tree of a given
/// labelled graph. Hint: Use the algorithm of Prim. A small modification of the solution of 
/// P83 does the trick. The data of the example graph to the right can be found in the file p84.dat.
/// 
/// Example:
/// 
/// <example in lisp>
/// 
/// Example in F#: 
/// > let graphW = [('a',['b'; 'd';]); ('b',['a';'c';'d';'e';]); ('c',['b';'e';]); 
///                 ('d',['a';'b';'e';'f';]); ('e',['b';'c';'d';'f';'g';]); ('f',['d';'e';'g';]); 
///                 ('g',['e';'f';]); ];;
/// > let gwF = 
///     let weigthMap = 
///         Map [(('a','b'), 7);(('a','d'), 5);(('b','a'), 7);(('b','c'), 8);(('b','d'), 9);
///              (('b','e'), 7);(('c','b'), 8);(('c','e'), 5);(('d','a'), 5);(('d','b'), 9);
///              (('d','e'), 15);(('d','f'), 6);(('e','b'), 7);(('e','c'), 5);(('e','d'), 15);
///              (('e','f'), 8);(('e','g'), 9);(('f','d'), 6);(('f','e'), 8);(('f','g'), 11);
///              (('g','e'), 9);(('g','f'), 11);]
///     fun (a,b) -> weigthMap.[(a,b)];;
/// 
/// val graphW : (char * char list) list =
///   [('a', ['b'; 'd']); ('b', ['a'; 'c'; 'd'; 'e']); ('c', ['b'; 'e']);
///    ('d', ['a'; 'b'; 'e'; 'f']); ('e', ['b'; 'c'; 'd'; 'f'; 'g']);
///    ('f', ['d'; 'e'; 'g']); ('g', ['e'; 'f'])]
/// val gwF : (char * char -> int)
/// 
/// > prim gw gwF;;
/// val it : char Graph =
///   (['a'; 'd'; 'f'; 'b'; 'e'; 'c'; 'g'],
///    [('a', 'd'); ('d', 'f'); ('a', 'b'); ('b', 'e'); ('e', 'c'); ('e', 'g')])
/// 

// [/snippet]


// [snippet: (**) Problem 85: Graph isomorphism]
/// Two graphs G1(N1,E1) and G2(N2,E2) are isomorphic if there is a bijection f: N1 -> N2 such
/// that for any nodes X,Y of N1, X and Y are adjacent if and only if f(X) and f(Y) are adjacent.
/// 
/// Write a predicate that determines whether two graphs are isomorphic. Hint: Use an open-ended
/// list to represent the function f.
/// 
/// Example:
/// 
/// <example in lisp>
/// 
/// Example in F#: 

// [/snippet]

// [snippet: (**) Problem 86: Node degree and graph coloration]
/// a) Write a predicate degree(Graph,Node,Deg) that determines the degree of a given node.
/// 
/// b) Write a predicate that generates a list of all nodes of a graph sorted according to 
///    decreasing degree.
/// 
/// c) Use Welch-Powell's algorithm to paint the nodes of a graph in such a way that adjacent 
///    nodes have different colors.
/// 
/// 
/// Example:
/// 
/// <example in lisp>
///
/// Example in F#: 
/// > let graph = [('a',[]);('b',['c']);('c',['b';'d';'g']);('d',['c';'e']);('e',['d';'e';'f';'g']);('f',['e';'g']);('g',['c';'e';'f'])];;
/// > degree graph 'e';;
/// val it : int = 5
/// > sortByDegree graph;;
/// val it : char Node list =
///   [ ('e',['d'; 'e'; 'f'; 'g']);  ('g',['c'; 'e'; 'f']);
///     ('c',['b'; 'd'; 'g']);  ('f',['e'; 'g']);  ('d',['c'; 'e']);
///     ('b',['c']);  ('a',[])]
/// val it : int = 5
/// > colorGraph graph;;
/// val it : (char * int) list =
///   [('a', 0); ('b', 1); ('c', 0); ('d', 1); ('e', 0); ('f', 2); ('g', 1)]

// [/snippet]

// [snippet: (**) Problem 87: Depth-first order graph traversal (alternative solution)]
/// Write a predicate that generates a depth-first order graph traversal sequence. The starting 
/// point should be specified, and the output should be a list of nodes that are reachable from 
/// this starting point (in depth-first order).
/// 
/// Example:
/// 
/// <example in lisp>
/// 
/// Example in F#: 
///
/// > let gdfo = (['a';'b';'c';'d';'e';'f';'g';], 
///               [('a','b');('a','c');('a','e');('b','d');('b','f');('c','g');('e','f');]) 
///               |> Graph2AdjacencyGraph;;
/// 
/// val gdfo : char AdjacencyGraph =
///   [('a', ['e'; 'c'; 'b']); ('b', ['f'; 'd'; 'a']); ('c', ['g'; 'a']);
///    ('d', ['b']); ('e', ['f'; 'a']); ('f', ['e'; 'b']); ('g', ['c'])]
/// 
/// > depthFirstOrder gdfo 'a';;
/// val it : char list = ['a'; 'e'; 'f'; 'b'; 'd'; 'c'; 'g']

// [/snippet]

// [snippet: (**) Problem 88: Connected components (alternative solution)]
/// Write a predicate that splits a graph into its connected components.
/// 
/// Example:
/// 
/// <example in lisp>
/// 
/// Example in F#: 
/// > let graph = [(1,[2;3]);(2,[1;3]);(3,[1;2]);(4,[5;6]);(5,[4]);(6,[4])];;
/// > connectedComponents graph;;
/// val it : int AdjacencyGraph list =
///   [[(6, [4]); (5, [4]); (4, [5; 6])];
///    [(3, [1; 2]); (2, [1; 3]); (1, [2; 3])]]
/// > 

// [/snippet]

// [snippet: (**) Problem 89: Bipartite graphs]
/// Write a predicate that finds out whether a given graph is bipartite.
/// 
/// Example:
/// 
/// <example in lisp>
/// 
/// Example in F#: 
///
/// > let gdfo = [('a', ['b'; 'c'; 'e']); ('b', ['a'; 'd'; 'f']); ('c', ['a'; 'g']);('d', ['b']); 
///               ('e', ['a'; 'f']); ('f', ['b'; 'e']); ('g', ['c'])];;
/// 
/// val gdfo : (char * char list) list =
///   [('a', ['b'; 'c'; 'e']); ('b', ['a'; 'd'; 'f']); ('c', ['a'; 'g']);
///    ('d', ['b']); ('e', ['a'; 'f']); ('f', ['b'; 'e']); ('g', ['c'])]
/// 
/// > isBipartite gdfo;;
/// val it : bool = true

// [/snippet]
