# Text Share
Programs to store and share text with rest API. 
Use cases: share text as a team, send text between ios and pc, repository of text
## Text Share Server
Hosting server for the service. 
Webinterface and swagger. 

### Build & Run
Built with ASP.NET Core targeting .NET Core 7.0.

Run directly from source with:
```sh
dotnet run [options]
```
Due to how dotnet works with running from source some short-hand options such as ```-p``` does not work, all long option variants does however still work.

To not have to build every time, or to not have dotnet interfere with the options, publish the project and then run:
<table>
<tr>
<td> Windows </td> <td> Linux / Mac </td>
</tr>
<tr>
<td>

```sh
dotnet publish
cd <PUBLISH_PATH> 
./TextShareServer.exe [options]
```
</td>
<td>

```sh
dotnet publish
cd <PUBLISH_PATH> 
dotnet TextShareServer.dll [options]
```
</td>
</tr>
</table>

#### Command line options

| Option       | Short | Function                                                           |
| ------------ | ----- | ------------------------------------------------------------------ |
| --http-port  | -p    | The port to listen for HTTP requests. Default value: '5000'.       |
| --https-port | -s    | The port to listen for HTTPS requests. HTTPS turned of if not set. |
| --no-http    |       | Turn off HTTP. Requires HTTPS port to be set.                      |
| --localhost  | -l    | Allow connection only from localhost.                              |
| --swagger    | -w    | Allows direct API access with Swaggr UI.                           |
| --help       |       | Display help text.                                                 |
| --version    |       | Display version information.                                       |

### API
Text Share Server exposes the following REST Api: 

<table>
<tr>
<td> Url </td> <td> Method </td> <td> Explanation </td> <td> Example calls & responses </td>
</tr>

<tr></tr>

<tr>
<td> /Text/ListIds </td> <td> GET </td> <td> Get all id's which have stored text.</td> 
<td>

```
/Text/ListIds
```

```
[Foo, Author]
```
</td>
</tr>

<tr></tr>

<tr>
<td> /Text/ListEntries </td> <td> GET </td> <td> Get all id's with their stored texts.</td> 
<td>

```
/Text/ListEntries
```

```json
[
    { "id": "Author", "text": "Robin Claesson\n" },
    { "id": "Foo", "text": "Bar\nHello World\n" }
]
```
</td>
</tr>

<tr></tr>

<tr>
<td> /Text/Peek/{id} </td> <td> GET </td> 
<td> Get the text stored with {id}. 
<p>
Leaves the text on the server</td> 
</p>
<td>

```
/Text/Peek/Foo
```

```
Bar
Hello World
```
</td>
</tr>

<tr></tr>

<tr>
<td> /Text/Pop/{id} </td> <td> GET </td> 
<td> Get the text stored with {id}. 
<p>
Deletes the text from the server</td> 
</p>
<td>

```
/Text/Pop/Foo
```

```
Bar
Hello World
```
</td>
</tr>

<tr></tr>

<tr>
<td> /Text/Push </td> <td> POST </td> 
<td> Stores "text" to "id". 
<p>
Appends to existing values for {id}.</td> 
</p>
<td>

```
/Text/Push
```

```json
{
  "id": "Foo",
  "text": "Bar"
}
```

```
Stored text 'Bar' to 'Foo'
```
</td>
</tr>

<tr></tr>

<tr>
<td> /Text/Delete/{id} </td> <td> DELETE </td> <td> Deletes the text stored for {id}.</td> 
<td>

```
/Text/Delete/Foo
```

```
Text for 'Foo' deleted
```
</td>

</table>

### Web Interface
The server also have a simple web page to show the currently stored texts.  

![Text Share Server web interface](/Assets/TextShareServerWeb.png)

#### Swagger
If the program is launched with the  ```--swagger``` option set, the API can be access in the web browser with [Swagger UI](https://swagger.io/tools/swagger-ui/) at ```/swagger/index.html```. The **Access API** link in the web interface leading to this page is only visible if Swagger is enabled.

![Text Share Server Swagger interface](/Assets/TextShareServerSwagger.png)