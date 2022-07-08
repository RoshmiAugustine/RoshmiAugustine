var fs = require('fs')

var someFile = './DbMigration/DbMigration.csproj';

fs.readFile(someFile, 'utf8', function (err,data) {
  if (err) {
    return console.log(err);
  }  

  var searchString = new RegExp('<EmbeddedResource Include="./Scripts/'+ process.argv[2] +'.sql" />', "g");
  var replaceString = '<EmbeddedResource Include="./Scripts/'+ process.argv[2] +'.sql" />\n        <EmbeddedResource Include="./Scripts/'+ process.argv[3] +'.sql" />';

  var result = data.replace(searchString, replaceString);

  fs.writeFile(someFile, result, 'utf8', function (err) {
     if (err) return console.log(err);
  });
});