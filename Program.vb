Imports System
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
'Imports RestSharp
Imports System.Text.Json
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Diagnostics.Eventing
Imports System.IO
Imports System.Text.Json.Nodes

Module Program
    Sub Main(args As String())



        Dim Token As String = GetAuthTokenAsync()

    End Sub
    'Async
    Function GetAuthTokenAsync() As String


        'Dim client = New HttpClient()
        Dim request = New HttpRequestMessage(HttpMethod.Post, "https://apis-sandbox.fedex.com/oauth/token")
        Dim list As List(Of KeyValuePair(Of String, String)) =
            New List(Of KeyValuePair(Of String, String))
        list.Add(New KeyValuePair(Of String, String)("client_id", "xxxxx"))
        list.Add(New KeyValuePair(Of String, String)("client_secret", "xxxx6"))
        list.Add(New KeyValuePair(Of String, String)("grant_type", "client_credentials"))
        Dim content As HttpContent = New FormUrlEncodedContent(list)

        content.Headers.ContentType = New MediaTypeHeaderValue("application/x-www-form-urlencoded")
        content.Headers.ContentType.CharSet = "UTF-8"

        Using client As HttpClient = New HttpClient()
            client.DefaultRequestHeaders.ExpectContinue = False
            Using response As HttpResponseMessage = client.PostAsync(New Uri("https://apis-sandbox.fedex.com/oauth/token"), content).Result

                Dim i = response.Content
                Using reader = New StreamReader(response.Content.ReadAsStream(), Encoding.UTF8)
                    Dim response2 = reader.ReadToEnd()
                    Dim bearer As JsonDocument = System.Text.Json.JsonSerializer.SerializeToDocument(response)

                    Dim forecastNode As JsonNode = JsonNode.Parse(response2)
                    Console.WriteLine(forecastNode("AccessToekn").ToString)
                    GetAuthTokenAsync = response2.ToString()
                End Using

                'Console.WriteLine(accessToken)
            End Using
        End Using

    End Function
End Module
