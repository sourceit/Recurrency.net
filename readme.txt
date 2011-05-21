Recurrency.net v0.3

WHAT IS RECURRENCY.NET
Recurrency.net (r.n) is a recurring date library for .net.

E.g.  You can use r.n to calcuate the 1st Wednesday in every month till Jan 1st 2020.



RECURRENCY PATTERNS

BASE PATTERN
Type 1 char					D | W | M | Y
Start date 8 chars			yyyymmdd
End date 8 chars			yyyymmdd, 00000000 if not specified, inclusive
Num recurrences 4 chars		0000 if not specified
Interval 4 chars			0000
Type specific info x chars


Notes:
Case insensitive
Spaces are removed before processing

DAILY SPECIFIC PATTERN
D...
Daily Type 1 char			X (Every x days) or W (weekdays)

E.g 

D 20111213 00000000 0012 0001 X

D			Daily
20111213	25 Dec 2011
00000000	No specified end date
0012		12 occurrences
0001		1 day apart
X           Every x days
 will calculate the 12 days of xmas for 2011

D 20110201 20110228 0000 0001 W
D			Daily
20110201	Feb 1st 2011
20110228	Feb 28th 2011
0000		No specifed num occurrences
0001		Not used, always treated as one for weekdays (for now)
W			Weekdays

will calculate every weekday in Feb 2011.

WEEKLY SPECIFIC PATTERN
W...
Use Monday			Y/N
Use Tuesday			Y/N
Use Wednesday		Y/N
Use Thursday		Y/N
Use Friday			Y/N
Use Saturday		Y/N
Use Sunday			Y/N

Note: Weeks start on mondays so that weekends stay together

E.g.

W 20110201 20110228 0000 0001 YNYNYNN
W			Weekly
20110201	Feb 1st 2011
20110228	Feb 28th 2011
0000		No specifed num occurrences
0001		Every week
YNYNYNN		Mon, Wed, Fri

MONTHLY SPECIFIC PATTERN
M... 
Monthly Type 1 char		W or M
00						Day number (day of month 1 based or day of week 0 based)
0						Weekday index 0 = 1st X, 3 = 4th X, 4 = last X

E.g.

M 20110201 20111130 0000 0001 M 14 0
M			Monthly
20110201	Feb 1st 2011
20110228	Nov 30th 2011
0000		No specifed num occurrences
0001		Every month
M			Day of month
14			14th
0			Not used
calculates the 14th of every month from Feb - Nov 2011

M 20110201 00000000 0006 0002 W 00 2 
M			Monthly
20110201	Feb 1st 2011
00000000	No specifed num occurrences
0006		6 occurrences
0002		Every 2nd month
W			Week day
00			Monday
2			3rd
calculates the 3rd Monday of every 2nd month from Feb 2011 for 6 occurrences

YEARLY SPECIFIC PATTERN
Y... 
Monthly Type 1 char		W or M
00						Day number (day of month 1 based or day of week 0 based)
0						Weekday index 0 = 1st X, 3 = 4th X, 4 = last X
00						Month of Year 1 = Jan
E.g.

Y 20110201 20150201 0000 0001 M 14 0 03
Y			Yearly
20110201	Feb 1st 2011
20150201	Feb 1st 2015
0000		No specifed num occurrences
0001		Every year
M			Day of month
14			14th
0			Not used
03			March
calculates March 14th of every year from Feb 2011 to Feb 2015

Y 20110201 00000000 0006 0002 W 00 2 04
Y			Yearly
20110201	Feb 1st 2011
00000000	No specifed num occurrences
0006		6 occurrences
0002		Every 2nd year
W			Week day
00			Monday
2			3rd
04			April
calculates the 3rd Monday in April of every 2nd year from Feb 2011 for 6 occurrences

