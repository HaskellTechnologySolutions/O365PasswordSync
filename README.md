# Buyer-Beware, this was designed for a specific use case. 99 times out of 100 you want to use Azure AD Connect

### Projects
* ConfigHandler: Library to somewhat securely store and load secrets. Not bulletproof, but good enough for the client
* Installer64: 64-Bit WIXv3 MSI Packager. Should be relatively easy to convert to 32-bit
* LSAProvider: The Windows LSA Password Filter DLL. Written in VS C++ unlike the rest of the project which is C#
* O365Client: The Office365 Graph API library handler
* SyncService: Windows Service that interacts with all of the above, main logic is here.
* SyncServiceConfig: Configuration Tool for the above service. Note that the AD specific service account username and password are no longer needed.
* TestSyncTool: Testing project for bashing together individual tests of the above.
