using System;
using System.IO;
using System.Reflection;

namespace VMD_GesInf.classes
{
    /**
     * Reads a given dcm file. 
     **/

    internal class DcmReader
    {
        public DcmReader(String url)
        {
            // DICOM files with a header have the ASCII signature "DICM" at byte offset 128.

            // When a header is present, the file begins with a 128-byte preamble that is usually set to all zero bytes, 
            // but which may be used for application-specific purposes. The next 4 bytes are the ASCII signature "DICM". 
            // Following the signature is a set of "Group 2" attributes, in little-endian, explicit-VR format. After the Group 2 attributes 
            // is the main part of the file, using the format given by the Transfer Syntax UID (0002,0010) attribute. 
            // ("Transfer Syntax" is the DICOM term for "file format".)

            // http://medical.nema.org/standard.html
            read(url);
        }

        private void read(String url)
        {
            // load embedded resource
//            System.Drawing.Icon icnTask;
//            System.IO.Stream st;
//            System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
//            st = a.GetManifestResourceStream("{{NameSpace}}.Resources.TaskIcon.ico");
//            icnTask = new System.Drawing.Icon(st); 
            // https://littletalk.wordpress.com/2010/02/18/how-to-load-an-icon-from-an-embedded-resource-in-c/
            // http://www.vcskicks.com/embedded-resource.php


//            Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("<assemblyName>.<foldername(optional)>.<filename>.<filenameExtention>");

            try
            {
                Stream stream =
                    Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("VMD_GesInf.resources.CR-MONO1-10-chest.dcm");
                if (stream == null)
                {
                    throw new FileNotFoundException(
                        "There was no embedded resource found for the given name: VMD_GesInf.resources.CR-MONO1-10-chest.dcm");
                }
                // The using statement also closes the StreamReader.
                using (StreamReader streamReader = new StreamReader(stream))
                {
//                    string line;
//                    while ((line = streamReader.ReadLine()) != null)
//                    {
//                        Console.Out.WriteLine(line);
//                    }                    

                    // read first 500 bytes
                    char[] buffer = new char[500];
                    int count = 0;
                    while (streamReader.Peek() >= 0 && count != 499)
                    {
                        buffer[count] = (char) streamReader.Read();
                        count++;
                    }
                    Console.Out.WriteLine(new String(buffer));
                }
//                    var read = streamReader.ReadBlock(buffer, 0, buffer.Length);
//                    Console.Out.WriteLine(read);
//                    var bufferString = new String(buffer);
//                    bufferString += "TEST kfjdhsfhsdfjhsdkfhkj";
//                    Console.Out.WriteLine(bufferString);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Some error occured: " + e);
            }
        }
    }
}