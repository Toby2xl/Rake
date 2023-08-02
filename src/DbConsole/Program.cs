
using System;
Console.WriteLine("Db updater starting ...........");

string authorization = $"Bearer GreatAPI";

string authTrim = authorization.Trim();

Console.WriteLine(authTrim);

int spaceIndex = authTrim.IndexOf(" ");
Console.WriteLine($"The index of space character in {authTrim} is {spaceIndex}");

var authSpan = authTrim.AsSpan();
Console.WriteLine($"Auth Span = {authSpan}");

var greatSpan = authSpan[spaceIndex..];

Console.WriteLine(greatSpan.ToString());

ReadOnlySpan<byte> textUtf8 = "Hello world"u8;

var spaceUtf8Index = textUtf8.IndexOf(" "u8);
Console.WriteLine($"The index of space character is {spaceUtf8Index}");
