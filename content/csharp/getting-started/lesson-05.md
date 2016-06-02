# Working with Dates and Times
by [Steve Smith](http://deviq.com/me/steve-smith)

#### Sample Files
Download a ZIP containing this tutorial's sample files:
- [Initial Version] - Use this as a starting point when following along with the tutorial yourself
- [Completed Version] - Includes the completed versions of all samples

## The DateTime Type

C# has built-in support for dates and times in the form of the ``System.DateTime`` type. This type can be used to represent a date and time, and it provides static access to the system clock which can be used to get the current time and/or date. The type also supports many operations that can make it easy to work with and manipulate dates and times.

The following block demonstrates some DateTime operations:

```c#
var currentTime = DateTime.Now; // current time
var today = DateTime.Today; // current date - time is midnight
var someDate = new DateTime(2016,7,1); // 1 July 2016, midnight
var someMoment = new DateTime(2016,7,1,8,0,0); // 1 July 2016, 08:00.00
var tomorrow = DateTime.Today.AddDays(1);
var yesterday = DateTime.Today.AddDays(-1);
var someDay = DateTime.Parse("7/1/2016");
```

Each of the above methods creates an instance of a DateTime.

## Formatting Dates and Times

There are many ways to display dates and times. Different countries and regions have different preferred formats for displaying dates. Times can be represented using 12-hour AM/PM style times, or using 24-hour or "military" times. The DateTime type supports many different options for converting a value into a string, along with support for custom formats.

## Getting to Parts of Dates and Times

DateTime types have many properties you can use to access different individual components of the date or time, such as the month, day, year, hour, minute, etc. The following sample demonstrates some of these properties:

```c#
var someTime = new DateTime(2016,7,1,11,10,9); // 1 July 2016 11:10.009 AM
int year = someTime.Year; // 2016
int month = someTime.Month; // 7
int day = someTime.Day; // 1
int hour = someTime.Hour; // 11
int minute = someTime.Minute; // 10
int milliseconds = someTime.Milliseconds; // 9
```

## Calculating Durations Between DateTimes

Show how to calculate the duration between two different Dates or Times.

Introduce TimeSpan type.

## Next Steps

To do.