# OTPManager

This is a web api , which generates 6 digit codes and validates the same for a given phone.

I have used [Otp.NET Nuget Package](https://www.nuget.org/packages/Otp.NET). This follows the TOTP (Timed One Time Password) algorithm.

The code is genertaed based on above said approach , and the validation is done against the cached code for the given phone. Here I have used In-Memory Cache for code persistence.

[GET] Endpoint generates the code.\
[POST] Endpoint validates the code.

I have enabled swagger , so any results can be viewed from swagger UI.

Added some unit test cases also. I have used XUnit , Moq and FluentAssertions for unit tests.

