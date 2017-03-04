# mol

## My Own Language - A toy Basic Language Interpreter

This is a Basic Language Interpreter, it is based on an PHP Based version dowloaded from

http://pub.32kb.org/files/entry/pBasic/pBasic.zip

written by  http://stackoverflow.com/users/2273749/felix-ruzzoli

pBasic.phrp is in turn s a port of the one file BASIC implementation in JAVA called jASIC to be found here:

http://journal.stuffwithstuff.com/2010/07/18/jasic-a-complete-interpreter-in-one-java-file

I've converted the PHP version to C# and improved it with the following features:

- Support for '<=', '>=' and '<>' operators, not present in the original version.
- Suport for "and" and "or" in the boolean expressions, not present in the original version.
- Support for if-else-endif structure in addition to the basic if then goto.
- Support for While-Wend.
- Support gor Gosub and retun.
- Support for sin, cos, int, round, trim, ltrin, rtrim and substr functions not present in the original version.

An example of the code interpreted by this interpretes is :

```basic

Print "Please enter a value"
Input a

i=0

DoWhile i <=10
  Exsr PrintEntry
  i=i+1
EndDo

EndPgm

BegSr PrintEntry

  Print i + " Multiplied by " + a + " equals " + (i*a)

EndSr


```
