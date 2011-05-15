WHAT IS RECURRENCY.NET
Recurrency.net (R.n) is a recurring date library for .net.

E.g.  You can use r.n to calcuate the 1st Wednesday in every month till Jan 1st 2020.



RECURRENCY PATTERN


Type 1 char					D | W | M | Y
Start date 8 chars			yyyymmdd
End date 8 chars			yyyymmdd, 00000000 if not specified, inclusive
Num recurrences 6 chars		000000 if not specified
Interval 6 chars			000000
Type specific info x chars


Notes:
Case insensitive
Spaces are removed before processing

DAILY SPECIFIC PATTERN
D...
Daily Type 1 char			X (Every x days) or W (weekdays)

E.g 

D 20111213 00000000 000012 000001 X

D			Daily
20111213	25 Dec 2011
00000000	No specified end date
000012		12 occurences
000001		1 day apart
X           Every x days
 will calculate the 12 days of xmas for 2011

D 20110201 20110228 000000 000001 W
D			Daily
20110201	Feb 1st 2011
20110228	Feb 28th 2011
000000		No specifed num occurences
000001		Not used, always treated as one for weekdays (for now)
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
E.g.

W 20110201 20110228 000000 000001 YNYNYNN
W			Weekly
20110201	Feb 1st 2011
20110228	Feb 28th 2011
000000		No specifed num occurences
000001		Every week
YNYNYNN		Mon, Wed, Fri