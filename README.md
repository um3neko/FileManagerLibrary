# FileManagerLibrary

The File Manager library is used to save and read files of different types (in this version .xml and .bin).
To add new format conversion methods, you need to create a typeHelper and implement the IConvertor interface in it.
In the FileFormatter class, when loading a new file type, add a new file extension to the switch construction, and also add a new type to the FileFormat enum.

The library has methods for working on a collection of files. It performs CRUD operations on files of a given structure on xml example.

![image](https://github.com/um3neko/FileManagerLibrary/assets/63252297/0c999e46-845c-41a7-8ff1-074953e7493b)

Using the methods of the FileFormatter class, we can initially create it (you will need to save it from memory to disk using the SaveFile method, which takes enum FileFormat and path.extension as a parameter).

We can also read a file into memory using the LoadFile method, which takes a path.extension string as a parameter.

Using the methods described below, we can perform operations on file data.

EditRecord(int recordIndex, Record record)
AddRecord(Record record)
DeleteRecord(int recordIndex)
------------------------------------------

In the client application describes examples of using the library

------------------------------------------
Unfortunately, for now, the library works in single-threaded mode.
But it is open to improvement.
