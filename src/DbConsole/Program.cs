using System.Text;

Console.WriteLine("Db updater starting ...........");
Console.WriteLine(GetIntersperse() == "1r2b8f7di4a061");
Console.WriteLine(ReplaceString());
Console.WriteLine(GetInter2() == "1r2b8f7di4a061");
Console.WriteLine(Hyphenate());

// string authorization = $"Bearer GreatAPI";

// string authTrim = authorization.Trim();

// Console.WriteLine(authTrim);

// int spaceIndex = authTrim.IndexOf(" ");
// Console.WriteLine($"The index of space character in {authTrim} is {spaceIndex}");

// var authSpan = authTrim.AsSpan();
// Console.WriteLine($"Auth Span = {authSpan}");

// var greatSpan = authSpan[spaceIndex..];

// Console.WriteLine(greatSpan.ToString());

// ReadOnlySpan<byte> textUtf8 = "Hello world"u8;

// var spaceUtf8Index = textUtf8.IndexOf(" "u8);
// Console.WriteLine($"The index of space character is {spaceUtf8Index}");

string webLogText = "Apr 10 11:17:35 coderbyte app/web.3: IP_MASKED - - [10/Apr/2020:18:17:35 +0000] \"GET /backend/requests/editor/placeholder?shareLinkId=69dff0hba0nv HTTP/1.1\" 200 148 \"https://coderbyte.com\" \"Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:74.0) Gecko/20100101 Firefox/74.0" +
"Apr 10 11:17:35 coderbyte heroku/router: at = info method = GET path = \"/backend/requests/editor/placeholder?key=s2fwad2Es2\" host = coderbyte.com request_id = b19a87a1 - 1bbb - 4e67 - b207 - bd9f23d46afa fwd = \"108.31.000.000\" dyno = web.3 connect = 0ms service = 92ms status=200 bytes=3194 protocol=https" +

"Apr 10 11:17:35 coderbyte heroku/router: at = info method = GET path = \"/backend/requests/editor/placeholder?shareLinkId=tosrve4v8q8q\" host = coderbyte.com request_id = 910b07d1 - 3f71 - 4347 - a1a7 - bfa20384ef65 fwd = \"108.31.000.000\" dyno = web.2 connect = 1ms service = 17ms status=200 bytes=4435 protocol=https" +

"Apr 10 11:17:35 coderbyte heroku/router: at = info method = GET path = \"/backend/requests/editor/placeholder?shareLinkId=tosrve4v8q8q\" host = coderbyte.com request_id = 097bf65e - e189 - 4f9f - 9dfb - 4758cff411b2 fwd = \"108.31.000.000\" dyno=web.3 connect=1ms service=10ms status=200 bytes=4435 protocol=https" +

"Apr 10 11:17:35 coderbyte app/web.2: IP_MASKED - - [10 / Apr / 2020:18:17:35 + 0000] \"GET /backend/requests/editor/placeholder?key=s2fwad2Es2 HTTP/1.1\" 200 4263 \"https://coderbyte.com\" \"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36" +

"Apr 10 11:17:35 coderbyte heroku/router: at = info method = GET path = \"/backend/requests/editor/placeholder?shareLinkId=4eiramcmayu0\" host = coderbyte.com request_id = d48278c2 - 5731 - 464e-be38 - ab9ad84ac4a8 fwd = \"108.31.000.000\" dyno = web.4 connect = 1ms service = 7ms status=200 bytes=3194 protocol=https" +

"Apr 10 11:17:35 coderbyte app/web.3: IP_MASKED - - [10 / Apr / 2020:18:17:35 + 0000] \"GET /backend/requests/editor/placeholder?shareLinkId=tosrve4v8q8q HTTP/1.1\" 200 4263 \"https://coderbyte.com\" \"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36" +

"Apr 10 11:17:35 coderbyte app/web.3: IP_MASKED - - [10 / Apr / 2020:18:17:35 + 0000] \"GET /backend/requests/editor/placeholder?shareLinkId=tosrve4v8q8q HTTP/1.1\" 200 4263 \"https://coderbyte.com\" \"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36" +

"Apr 10 11:17:36 coderbyte app/web.4: IP_MASKED - - [10 / Apr / 2020:18:17:35 + 0000] \"GET /backend/requests/editor/placeholder?shareLinkId=4eiramcmayu0 HTTP/1.1\" 200 3023 \"https://coderbyte.com\" \"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.163 Safari/537.36" +

"Apr 10 11:17:36 coderbyte heroku/router: at = info method = GET path = \"/backend/requests/editor/placeholder?shareLinkId=tosrve4v8q8q\" host = coderbyte.com request_id = 8bb2413c - 3c67 - 4180 - 8091 - 000313b8d9ca fwd = \"MASKED\" dyno=web.3 connect=1ms service=32ms status=200 bytes=4435 protocol=https" +

"Apr 10 11:17:36 coderbyte heroku/router: at = info method = GET path = \"/backend/requests/editor/placeholder?shareLinkId=tosrve4v8q8q\" host = coderbyte.com request_id = 10f93da3 - 2753 - 48a3 - 9485 - 857a93d8a88a fwd = \"MASKED\" dyno=web.3 connect=1ms service=37ms status=200 bytes=4435 protocol=https";


var logArray = webLogText.Trim().Split("Apr 10 11:");
int noUniqueIdCount = 0;
int uniqueCount = 0;
var dict = new Dictionary<string, int>();
for (var i = 0; i<= logArray.Length - 1; i++)
{
    // if(string.IsNullOrEmpty(logArray[i]))
    // {
    //     continue;
    // }

    if(!logArray[i].Contains("shareLinkId"))
    {
        noUniqueIdCount++;
        continue;
    }
    else
    {
        string uniqueIdValue = GetUniqueId(logArray[i].ToString());
        if (dict.ContainsKey(uniqueIdValue))
        {
            dict[uniqueIdValue]++;
        }
        else
        {
            dict[uniqueIdValue] = 1;
        }
        uniqueCount++;
    }
}

Console.WriteLine($"noUniqueIdCount = {noUniqueIdCount}, UniqueIdCount = {uniqueCount}, Total = {logArray.Length}");
Console.WriteLine("Printing The occurence and their times .....");
foreach (var item in dict)
{
    Console.WriteLine("{0}:{1}",item.Key,item.Value);
}

static string GetUniqueId(string data)
{
    int startIndex = data.IndexOf("shareLinkId=");
    //if(startIndex != -1)
    //{
    startIndex += "shareLinkId=".Length;
    int endIndex = data.IndexOf(" ", startIndex);
    if(endIndex != -1)
    {
        string shareLinkId = data.Substring(startIndex, endIndex - startIndex);
        return shareLinkId.Trim('"');
    }
    //}

    return string.Empty;
}

static string GetIntersperse(string age = "128", string chalToken = "rbf7di4a061")
{
    //output = 1r2b8f7di4a061
    var ageArray = age.ToCharArray();
    var tokenArray = chalToken.ToCharArray();
    int finalLent = ageArray.Length + tokenArray.Length;
    int count = 0;
    var builder = new StringBuilder();
    while(count != finalLent)
    {
        if(count < ageArray.Length)
        {
            builder.Append(ageArray[count]);
        }
        if(count < tokenArray.Length)
        {

            builder.Append(tokenArray[count]);
        }
        count++;
    }
    return builder.ToString();
}
static string GetInter2(string age = "128", string chalToken = "rbf7di4a061")
{
    string finalOutput = "";
    string fusedString = age + chalToken;
    for (int i = 0; i < fusedString.Length; i++)
    {
        if(i < age.Length)
        {
            finalOutput += age[i];
        }
        if(i < chalToken.Length)
        {
            finalOutput += chalToken[i];
        }
    }
    return finalOutput;
}

static string ReplaceString(string age = "128", string chalToken = "rbf7di4a061")
{
    string finalString = "";
    var fusedString = age + chalToken;
    int count = 1;
    foreach (var item in fusedString)
    {
        if(count % 3 == 0)
        {
            finalString += "X";
        }
        else
        {
            finalString += item;
        }
        count++;
    }
    return fusedString + "<==>" + finalString;
}

static string Hyphenate(string age = "128", string chalToken = "rbf7di4a061")
{
    string finalString = "";
    string fusedString = age + chalToken;
    var array = fusedString.ToCharArray();
    foreach (var item in array)
    {
        if(!char.IsDigit(item))
        {
            finalString += "-";
        }
        else
        {
            finalString += item;
        }
    }
    return finalString;
}
