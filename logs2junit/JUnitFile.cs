using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace logs2junit
{
   class JUnitFile
   {
      private static StreamWriter sw;

      public static StreamWriter Sw { get => sw; set => sw = value; }

      public JUnitFile() { }

      public bool Create( string strFileTargetPath )
      {
         try
         {
            Directory.CreateDirectory(Path.GetDirectoryName(strFileTargetPath));
            Sw = new StreamWriter(File.Open(strFileTargetPath, FileMode.Create), Encoding.Unicode);
            return true;
         }
         catch (Exception e)
         {
            Console.WriteLine("Error while creating the .xml file: " + e.Message);
            return false;
         }
      }

      public bool Header(string strTestType, int iTotalTests, int iFailedTests)
      {
         try
         {
            Sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-16\"?>");
            Sw.WriteLine("<testsuite name=\"" + strTestType + "\" errors=\"0\" tests=\"" + iTotalTests + "\" failures=\"" + iFailedTests + "\" time=\"\" timestamp=\"\">");
            return true;
         }
         catch (Exception e)
         {
            Console.WriteLine("Error while writing the header: " + e.Message);
            return false;
         }
      }

      public bool AddTests(string strModuleTestName, int iTotalTests, string strTotalTestsFile, int iFailedTests, string strFailedTestsFile, string strErrorsFile)
      {
         List<string> FailedTests = null;

         if (iFailedTests > 0)
         {
            if (strFailedTestsFile == null)
            {
               for (int index = 0; index < iFailedTests; ++index)
               {
                  if (!AddFailedTest(strModuleTestName, "Test" + (index + 1), ""))
                  {
                     return false;
                  }
               }
            }
            else
            {
               var logFile = File.ReadAllLines(strFailedTestsFile);
               FailedTests = new List<string>(logFile);

               if (FailedTests.Count() == 0)
               {
                  for (int index = 0; index < iFailedTests; ++index)
                  {
                     FailedTests.Add("Test" + (index + 1));
                  }
               }

               for (int index = 0; index < iFailedTests; ++index)
               {
                  string strFailedTest = FailedTests.ElementAt(index);
                  string strError = "";
                  if (strErrorsFile != null)
                  {
                     StreamReader ErrorsFile = new StreamReader(strErrorsFile);
                     while (!ErrorsFile.EndOfStream)
                     {
                        string strLine = ErrorsFile.ReadLine();
                        if (strLine.Contains(strFailedTest))
                        {
                           strError += strLine.Replace(strFailedTest + " ", "") + "\n";
                        }
                     }
                  }
                  if (!AddFailedTest(strModuleTestName, strFailedTest, strError))
                  {
                     return false;
                  }
               }
            }
         }

         if (strTotalTestsFile == null)
         {
            for (int index = 0; index < iTotalTests - iFailedTests; ++index)
            {
               if (!AddPassedTest(strModuleTestName, "Test" + (index + 1)))
               {
                  return false;
               }
            }
         }
         else
         {
            string[] strLines = File.ReadAllLines(strTotalTestsFile);
            if (strLines.Count() == 0)
            {
               strLines = new string[iTotalTests];
               for (int index = 0; index < iTotalTests; ++index)
               {
                  strLines.SetValue("Test" + (index + 1), index);
               }
            }
            for (int index = 0; index < iTotalTests; ++index)
            {
               if (!FailedTests.Contains(strLines[index]))
               {
                  if (!AddPassedTest(strModuleTestName, strLines[index]))
                  {
                     return false;
                  }
               }
            }
         }

         return true;
      }

      public bool AddPassedTest(string strModuleTestName, string strTest)
      {
         try
         {
            Sw.WriteLine("<testcase classname=\"" + strModuleTestName + "\" name=\"" + strTest + "\" time=\"\" />");
            return true;
         }
         catch (Exception e)
         {
            Console.WriteLine("Error while adding test " + strTest + " Error message: " + e.Message);
            return false;
         }
      }

      public bool AddFailedTest(string strModuleTestName, string strTest, string strError)
      {
         try
         {
            Sw.WriteLine("<testcase classname=\"" + strModuleTestName + "\" name=\"" + strTest + "\" time=\"\">");
            Sw.WriteLine("<error message=\"Test failed! Check ATS Test Results for more details.\" type=\"Assertion/Timeout\">" + strError + "</error>");
            Sw.WriteLine("</testcase>");
            return true;
         }
         catch (Exception e)
         {
            Console.WriteLine("Error while adding test " + strTest + " Error message: " + e.Message);
            return false;
         }
      }

      public bool Footer()
      {
         try
         {
            Sw.WriteLine("</testsuite>");
            Sw.Close();
            return true;
         }
         catch (Exception e)
         {
            Console.WriteLine("Error while writing the footer: " + e.Message);
            return false;
         }
      }
   }
}