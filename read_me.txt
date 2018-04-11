How does this work

Project: LLU_Service 2 is the full source for the service
- ClassLibrary: Library.cs
---- is the generic library for the service to WRITE to logfile 

- ClassLibrary: LibrarySQL.cs
---- contains only methods for SQL calls

- Class : Scheduler.cs
---- contains the actual scheduler that'll call Library/LibrarySQL functions/methods

Project: Test_LLU_Service2 is only for verifying the SQL call within the service.