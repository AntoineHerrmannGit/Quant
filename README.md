# Quant
-------
This project is about to explore Quantitative Trading Strategies through a in-dev trading infrastructure and custom strategies.
Strategies emerge from market data analysis and personnal ideas that I encourage to be suggested and raised.

Along with the development, unitary tests must be written to ensure the code's consistency and help detect errors.
Please run, check and fix them before each pul request. 

The project contains multiple modules, each one responsile of a single functionnality

# Buonaparte
------------
Buonaparte is the main service responsible of exposing API endpoints to microservices and modules.
As the main character of the Great Army, he leads and manages services and provides for the rest of the project

# Ney
-----
Ney is the calculator of the army. It contains the pricing tools and the calculus libraries.

# Berthier
----------
Berthier is a DataBase service in charge of gathering data from web ressources and maintaining a local database.

Berthier uses multiple software ressources : 
- Redis for cache management, 
- SQL databases for data storage and accessibility
- File management
- URL requests 

# Models
--------
Contains all DataStructures used in the entire code. Only constructors, accessors and operators overload must be declared in the models. 
Associated features must be written in Extensions.

Enums are treated apart due to their special nature.

# Tests
-------
Tests contains all tests to be run at each pull request. 
All tests muse succeed for pull request approval.

# Tools
-------
Tools are mainly math-based methods, functions and analytical methods and algorithms that are common to all services.
