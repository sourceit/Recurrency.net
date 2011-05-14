WHAT IS RECURRENCY.NET
Recurrency.net (R.n) is a recurring date library for .net.

E.g.  You can use r.n to calcuate the 1st Wednesday in every month till Jan 1st 2020.



TEXT REPRESENTATION OF RECURRENCY


Type 1 char					D | W | M | Y
Start date 8 chars			yyyymmdd
End date 8 chars			yyyymmdd, 00000000 if not specified, inclusive
Num recurrences 6 chars		000000 if not specified
Type specific info x chars


E.g 

D2011121300000000000012X000001

D			Daily
20111213	25 Dec 2011
00000000	No specified end date
000012		12 occurences
X           Every x days
000001		1 day apart
 will calculate the 12 days of xmas for 2011

D2011020120110228000000W
D
20110201	Feb 1st 2011
20110228	Feb 28th 2011
000000		No specifed num occurences
W			Weekdays
will calculate 