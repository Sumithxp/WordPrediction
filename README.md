# Overview
Get word suggestions (predictions) while typing. Predictions are word completions based on the input text, for example: If the input text is I like ca the predictions could be cars, cats and cake.

The application is built by .Net 6 Web Api and VueJs following the principle of clean architecture

# Back-end
Provides Web Api to get list of prediction words for given text.

**Tools**
.NET 6,
Entity Framework core,
SqlLite 3

# Front-end
1. Write HTML/CSS for displaying a <textarea> and two lists of word prediction results (word prediction web service and custom dictionary).
2. Vue Js used to function that performs a web request to fetch word predictions from your back-end API.
3. On every keypress event in the <textarea> fetch word predictions based on the text value and display the results in the two lists.

**Tools**
VUE js,
HTML5,
CSS3

###### Usage
  
`> npm install --global serve`
  
`> serve`
  
