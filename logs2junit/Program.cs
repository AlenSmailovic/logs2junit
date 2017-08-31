using System;

namespace logs2junit
{
   class Program
   {
      static int Main(string[] args)
      {
         string strTestType         = null;     //ex.: Regression Tests
         string strModuleTestName   = null;     //ex.: KernelSystem
         int iTotalTests            = 0;        //ex.: 10
         int iFailedTests           = 0;        //ex.: 5
         string strFileTargetPath   = null;     //ex.: D:\TestsDir\Name.xml
         string strTotalTestsFile   = null;     //ex.: D:\TestsDir\KernelSystemTotalTests.log
         string strFailedTestsFile  = null;     //ex.: D:\TestsDir\KernelSystemFailedTests.log
         string strErrorsFile       = null;     //ex.: D:\TestsDir\KernelSystemErrors.log

         if (!ArgsMain(args, ref strTestType, ref strModuleTestName, ref iTotalTests, ref iFailedTests, ref strFileTargetPath, ref strTotalTestsFile, ref strFailedTestsFile, ref strErrorsFile))
         {
            return 1;
         }

         JUnitFile File = new JUnitFile();

         if (!File.Create(strFileTargetPath))
         {
            return 2;
         }
         if (!File.Header(strTestType, iTotalTests, iFailedTests))
         {
            return 3;
         }
         if (!File.AddTests(strModuleTestName, iTotalTests, strTotalTestsFile, iFailedTests, strFailedTestsFile, strErrorsFile))
         {
            return 4;
         }
         if (!File.Footer())
         {
            return 5;
         }

         return 0;
      }

      private static bool ArgsMain(string[] args, ref string strTestType, ref string strModuleTestName, ref int iTotalTests, ref int iFailedTests, ref string strFileTargetPath, ref string strTotalTestsFile, ref string strFailedTestsFile, ref string strErrorsFile)
      {
         if (args.Length == 1 && (args[0].Contains("/?") || args[0].Contains("/help")))
         {
            Console.WriteLine("--> About");
            Console.WriteLine("Create JUunit .xml file format using test logs");
            Console.WriteLine("");
            Console.WriteLine("--> Syntax");
            Console.WriteLine("logs2junit.exe [test-type] [module-test-name] [total-tests] [failed-tests] [target-file-path] [opt-param:total-tests-file-path] [opt-param:failed-tests-file-path] [opt-param:error-tests-file-path]");
            Console.WriteLine("");
            Console.WriteLine("opt = optional parameters. The tool will work even if you provide null path, or empty files for opt-param. This is useful when you don't have logs from a test but you still want to create a .xml. with numbers of tests passed / failed.");
            Console.WriteLine("* The first two opt-param are preliminary if you want to have all the tests (passed / failed) for a module, by name. If is not provided / the file path is null / file is empty, the name for test will be Test(#).");
            Console.WriteLine("* The last opt-param is needed only you want to have the errors for each test which have failed. If is not provided / the file path is null / file is empty, the error field will be blank.");
            Console.WriteLine("");
            Console.WriteLine("[test-type] = the type of the tests. Example: Regression Tests");
            Console.WriteLine("[module-test-name] = the title for the tests. Example: Performance");
            Console.WriteLine("[total-tests] = the number of all tests (passed + failed)");
            Console.WriteLine("[failed-tests] = the number of failed tests");
            Console.WriteLine("[target-file-path] = the name for the .xml file. Example: D:\\target-path\\tests\\PerformanceTest.xml");
            Console.WriteLine("[total-tests-file-path] = a text file which contains all the tests which have ran. Example: D:\\logs\\Performance_TotalTests.log");
            Console.WriteLine("[failed-tests-file-path] = a text file which contains all the tests which have failed. Example: D:\\logs\\Performance_FailedTests.log");
            Console.WriteLine("[error-tests-file-path] = a text file which contains all the errors for each test. The error should be one-per-line with the name of test at begining of line. Example: D:\\logs\\Performance_TotalTests.log");
            return false;
         }

         if (args.Length < 5)
         {
            Console.WriteLine("Provide necessary arguments. Use /? or /help for explanations.");
            return false;
         }

         try
         {
            strTestType = args[0];
            strModuleTestName = args[1];
            iTotalTests = Int32.Parse(args[2]);
            iFailedTests = Int32.Parse(args[3]);
            strFileTargetPath = args[4];

            if (args.Length == 7)
            {
               if (System.IO.File.Exists(args[5]))
               {
                  strTotalTestsFile = args[5];
               }
               if (System.IO.File.Exists(args[6]))
               {
                  strFailedTestsFile = args[6];
               }
            }
            else if (args.Length == 8)
            {
               if (System.IO.File.Exists(args[5]))
               {
                  strTotalTestsFile = args[5];
               }
               if (System.IO.File.Exists(args[6]))
               {
                  strFailedTestsFile = args[6];
               }
               if (System.IO.File.Exists(args[7]))
               {
                  strErrorsFile = args[7];
               }
            }
            else
            {
               Console.WriteLine("To many arguments. Use /? or /help for explanations.");
               return false;
            }
            return true;
         }
         catch (Exception e)
         {
            Console.WriteLine("Error while reading the arguments: " + e.Message);
            return false;
         }
      }
   }
}