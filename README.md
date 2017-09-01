## logs2junit
### About
Easily create JUnit .xml files from test logs.
Download the tool [HERE] (https://github.com/AlenSmailovic/logs2junit/blob/master/logs2junit/bin/Release/logs2junit.exe?raw=true) ( x64 version ) - for x86, please download the repo and build it :)

### Command
logs2junit.exe [test-type] [module-test-name] [total-tests] [failed-tests] [target-file-path] [opt-param:total-tests-file-path] [opt-param:failed-tests-file-path] [opt-param:error-tests-file-path]

#### Example:
> **logs2junit.exe** "Regression" "Kernel System" 10 2 D:\test\KernelSystemTest.xml D:\test\KernelSystem_TotalTests.log D:\test\KernelSystem_FailedTests.log D:\test\KernelSystem_ErrorTests.log

### Syntax
**[test-type]** = the type of the tests. Example: Regression Tests

**[module-test-name]** = the title for the tests. Example: Performance

**[total-tests]** = the number of all tests (passed + failed)

**[failed-tests]** = the number of failed tests

**[target-file-path]** = the name for the .xml file. Example: D:\\target-path\\tests\\PerformanceTest.xml

**[total-tests-file-path]** = a text file which contains all the tests which have ran. Example: D:\\logs\\Performance_TotalTests.log

**[failed-tests-file-path]** = a text file which contains all the tests which have failed. Example: D:\\logs\\Performance_FailedTests.log

**[error-tests-file-path]** = a text file which contains all the errors for each test. The error should be one-per-line with the name of test at begining of line. Example: D:\\logs\\Performance_TotalTests.log

### Details
opt = optional parameters;
The tool will work even if you provide null path, or empty files for opt-param. This is useful when you don't have logs from a test but you still want to create a .xml. with numbers of tests passed / failed.

The first two opt-param are preliminary if you want to have all the tests (passed / failed) for a module, by name. If is not provided / the file path is null / file is empty, the name for test will be Test(#).

The last opt-param is needed only you want to have the errors for each test which have failed. If is not provided / the file path is null / file is empty, the error field will be blank.

**Check test folder to see how total-test-file, failed-test-file and error-test-file showld look.**

For more details, please contact me ( alen.smailovic@gmail.com )

**Enjoy!**
