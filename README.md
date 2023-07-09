# FileManagerLibrary

The File Manager library is used to save and read files of different types (in this case .xml and .bin).
To add new format conversion methods, you need to create a typeHelper and implement the IConvertor interface in it.
In the FileFormatter class, when loading a new file type, add a new file extension to the switch construction, and also add a new type to the FileFormat enum.

The library has methods for working on a collection of files. It performs CRUD operations on files of a given structure on xml example.

<Document>
  <Cars>
    <Car>
      <Date>2023-07-09T19:24:48.792245Z</Date>
      <BrandName>Brand 5</BrandName>
      <Price>500</Price>
    </Car>
  </Cars>
</Document>


Using the methods of the FileFormatter class, we can initially create it (you will need to save it from memory to disk using the SaveFile method, which takes enum FileFormat and path.extension as a parameter).

We can also read a file into memory using the LoadFile method, which takes a path.extension string as a parameter.

Using the methods described below, we can perform operations on file data.
EditRecord(int recordIndex, Record record)
AddRecord(Record record)
DeleteRecord(int recordIndex)
------------------------------------------
Unfortunately, for now, the library works in single-threaded mode.
But she is open to improvement.
