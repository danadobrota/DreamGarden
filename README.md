# Dev Academy Project "DreamGarden"
# Project: Order Management Platform for an Online Store!


# Short description
This project involves building a web application that allows users to browse products, place orders, and manage their purchases. It will be designed following Object-Oriented Programming (OOP) principles, leverage SQL Server for data persistence, expose functionalities via a RESTful API, and implement design patterns for maintainability and scalability. The project also requires integration with Postman/Fiddler for API testing and optimizations to improve performance.

For this project I have selected the ASP.NET Core Web App MVC template.

(./doc/template.png)




ASP.NET MVC is a framework for building web applications using the Model-View-Controller (MVC) design pattern. It's a lightweight, highly testable framework, and it's open-source apart from the Web Forms component, which is proprietary. 

MVC Pattern contains the following folders I have expanded and worked on :
• Model: Represents the data and business logic of the application -> here we retrieve information from the database, operated on it and then write the information back to SQL
• View: Displays the data to the user, worked on Razor  for displaying the user interface.
• Controller: Handles user input, interacts with the model, and chooses which view to display. 

					

# Administrative 

For this project, I have combined my passion for gardening with my technical skills. The database catalog features products and photos, all of which are sourced from my own home garden.
The images have been loaded inside the img folder of the ASP.NET wwwroot folder 



Due to the fact that these images were taking a lot of space when I started my initial commits, my project has exceeded the 100MB size.
I had issues committing multiple changes between 8-26 June.
This is the reason why there  is a huge gap inside my github Repository from 8 June…26 June. 
I continued my work constantly on my local VSO while trying to
-compress the images
-cleaned the solution+soft deleting
-usage of Git Large Files storage and tracking the problematic folders with the help of mentor.
None of these methods proved to be efficient, on 26 June I have uploaded the current state of build and eliminated the Dreamgarden.git folder, this is the reason why the successive commits are followed up upon the data 26 June.



History of Github







For the authentication part I have used the ASP.NET Core Identity API to support the UI login functionality.

