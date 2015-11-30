# steam
An automated process that search for image and video urls.

The way it works is that there are a list of JSON files that contains links to video files and images. When the program starts 
it ask you what JSON file you want to check. Once the JSON file is selected goes through each parameter, checks through the
video or image parameter. Downloads the files and display the results in a newly created folder. The process it takes to download
is determine by how info is in the JSON file. There are a couple of error checks like checking if the url containing the video
or image file is broken. I'm hoping to work on a update later in the future to make searching easier. 
