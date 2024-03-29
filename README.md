# Garage2.0

Group assignment by Lexicon.
This is the second version of the Garage console application that is built upon using ASP.NET MVC framework and Entity framework.


## Table of Contents

- [Clone](#clone)
- [Install](#install)
- [Start](#start)
- [Usage](#usage)

## Clone

You can clone this repository for your own uses through the following command in your terminal:
```
cd <your-chosen-folder>
git clone https://github.com/usk1129/Garage2.0
```

## Install

After downloading the code, open the solution with Visual Studio.

To seed the database with the seed-data created, the following command must be executed in the "Package Manager Console" of Visual Studio:
```
update-database -context Garage2_0Context
```

## Start

Once the solution is opened in Visual Studio it can easily be run through Visual studio. A server should be started and the browser be opened automatically.

## Usage

The goal of this garage application is to register different types of vehicles and that the different vehicles should take different place(many to many). 
There must also be registered members and one member can check in several vehicles and one vehicle can only have one owner / member(one to many). We register a member by a person-number.
This repo represents our implementation of the garage application.
![Untitled Diagram drawio](https://user-images.githubusercontent.com/32932279/157126429-f9d096cb-51f2-46a1-b847-c1c9585756eb.svg)

We have 4 Entities with it's own attributies.

We have made it possible for you to add different types of vehicles and to specify how many slots the specified vehicle should take and the opportunity to select the existing member when you check in. 
There are also some other overviews for members with parked vehicles or not and how many. Of course view modules for vehicles, static information and etc 
There are also search and sort functions for members and parked vehicles.

When you check out, there is the possible to check the time how long it has been parked automatically and to get the receipt for it.
