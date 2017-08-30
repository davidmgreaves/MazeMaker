# MazeMaker
## A Unity project exploring procedural generation with maze algorithms

A perfect maze is a maze with only one possible solution and no circular paths. This project demonstrates the implementation of a variety 
of algorithms borrowed from graph theory, that can be used to procedurally generate a perfect maze. To see this project in action, click
through to the web player here (Link coming soon). You can use the dropdown to toggle between three seperate algorithms to create a maze,
each with it's own set of characteristics. 

**N.B- this project is still under construction, though the source code hosted to date will
generate a perfect maze in the Unity Editor using the Recursive Backtracking Algorithm. Updates and webplayer link soon to follow!**

### Recursive Backtracking Algorithm
This is the simplest of the algorithms to implement, and is my recommended starting point for anyone interested in attempting their own
implementation. It also tends to be the most visually appealing with long passages and fewer dead ends than the other algorithms. To 
start with you will need to create a collection of floor tiles (or cells as I have named them in my code), each surrounded by four walls, and 
arrange them in a grid of columns and rows. Then a basic overview of the algorithm is as follows:

1. Choose a random starting point from the collection of tiles, and add this tile to a collection of visited tiles for backtracking 
purposes.

2. Randomly choose one of the neighbouring tiles to move to (directly above, below, to the left or to the right) and add it to the 
collection of visited tiles, before destroying the adjoining wall between them

3. If the new cell has one or more unvisited neighbouring tiles, go back to step 2, otherwise proceed to step 4

4. If there are any tiles remaining in the collection of visited tiles, select the tile most recently added and go back step 3, otherwise
the algorithm is complete and you have a perfect maze!


### Hunt And Kill Algorithm
This was the first algorithm I attempted and thus remains one of my favorites. It also has by far the coolest name. Similar to the 
Recursive Backtracking Algorithm this maze involves carving a path through a grid of walled cells. Instead of keeping track of previously
visited cells and backtracking every time it hits a dead end, this algorithm checks each row one cell at a time for an unvisited cell, 
with a visited cell that is vertically or horizontally adjacent to it.

1. Choose a random starting point from the collection of tiles, and add this tile to a collection of visited tiles for validation 
purposes.

2. Randomly choose one of the neighbouring tiles to move to (directly above, below, to the left or to the right) and add it to the 
collection of visited tiles, before destroying the adjoining wall between them

3. If the new cell has one or more unvisited neighbouring tiles, go back to step 2, otherwise proceed to step 4

4. Starting with the first row, check each row sequentially for an unvisited cell, with a neighbouring tile that has been visited. 

5. If you found a cell as described in step 4, check for one or more unvisited neighbours, if you find any, go back to step 2, otherwise
go back to step 4, continuing your search from where you left off. If you did not find any cells in step 4, the algorithm is complete
and you once again have a perfect maze!

### Recursive Division
Unlike the last two algorithms which are destructive in that they involve destroying gameobjects to carve a path through a grid of cells
with walls, this algorithm starts with a grid of cells without walls (except for the outer walls forming the perimeter for the maze, and
adds one wall at a time, recursively dividing the available space until a maze is created at a resolution specified by the number of
columns and rows.

1. Starting with your grid of empty cells encapsulated by a surrounding wall, divide the given space either horizontally or vertically
by adding enough wall objects to span the entire distance of the maze

2. Delete one of the wall objects to create a single passage through the wall

3. Divide the space again on either side of the previous wall, once again carving a passage through the new wall by destroying one wall

4. Repeat step 3 recursively until the area you are working on has reached the desired resolution (you can not add any more walls 
without increasing the number of columns or rows specified prior to creating the maze). Move to any remaining space in the maze that is
not yet at the desired resolution and continue until no such spaces remain and you have a perfect maze


### Ellers Algorithm
Ok I know I said there were only three algorithms to choose from in my project, but I am considering attempting a fourth one at a later 
date. Ellers Algorithm is said to be among the most complex algorithms available, and utilises set theory to create the final product.

For an indepth discussion of the algorithms listed above along with several others, I highly recommend checking out [TheBuckBlog](http://weblog.jamisbuck.org/2011/1/12/maze-generation-recursive-division-algorithm)
by Jamis Buck. He is super knowledgeable on the subject, and does a far better job of explaining the algorithms (and in great detail)
than I do. He loves mazes so much he took his fascination with maze algorithms as far as writing a book dedicated to the subject- 
[Mazes For Programmers](http://www.mazesforprogrammers.com/) which can be purchased on Amazon. He also has an interactive intro to the
topic of maze algorithms [here](http://jamisbuck.org/presentations/rubyconf2011/#title-page).
