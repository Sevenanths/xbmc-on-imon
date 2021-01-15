# XBMC on iMON Display

**UPDATE by Sevenanths (July 2020)**

![Screenshot](docs/screenshot.png)

I have updated XBMC on iMON Display for the new decennium. It was not a pleasant experience.  
For future generations, these are the steps I undertook in order to be able to update the program:

I decompiled the program and all DLLs (libraries) using [DotPeek](https://www.jetbrains.com/decompiler/). It was a Christmas miracle in July that the output was legible, and everything still compiled in Visual Studio. When recompiling, it is important to set the platform target to x86 specifically. Else, the iMon library will cause errors. Target platforms for both the program and its libraries were set to .NET Framework 4.5. 

The NewtonSoft JSON library was replaced by its .NET 4.5 counterpart. Contrary to my expectations, this was a drop-in replacement.

I fixed some decompilation mistakes in XBMC on iMon itself, but these changes were very minor.

More or less all changes were made in the `XBMCJsonRpcSharp` class. In the `XbmcJsonRpcConnection.cs` file, I initialised a websocket connection to Kodi (in 2020, we call XBMC *Kodi* now). The reason why the program didn't work as-is anymore with Kodi 18 was that - for a reason unknown to me - so-called *notifications* from Kodi didn't reach XBMC on iMon anymore. Using websockets, we can still receive these notifications. I used [this websocket wrapper](https://gist.github.com/xamlmonkey/4737291) by Christiaan Coetzer.

In addition, I had to fix what seems like a race condition which prevented the library from hooking to an active player. After the library receives the "Player.OnPlay" event, it immediately calls for an active player to hook to it. In newer versions of Kodi, several "Player.OnAVChange" events follow the "OnPlay" event, and a player is only advertised after these events have occurred. If the program tries to call for an active player immediately, the subsequent events will not yet have been sent, and it will therefore fail to hook. I added a 500ms delay which solved this problem. With the speed at which VFDs update, you will likely not notice this. 

Finally, "Play" and "Pause" states and icons would not work until I hooked the OnResume event in `XbmcJsonRpcConnection.cs`. I don't even know how the program worked without it.

This whole project is terrible and I hate iMon. Please don't ask for support. I will lose my sanity.

---------------

Table of Contents
=================
1. Hardware Requirements
2. Software Requirements
3. Installation
4. Preparations
5. Usage
6. Known Issues
7. Support
8. Disclaimer


## 1. Hardware Requirements
To be able to use "XBMC on iMON Display" you need an LCD or VFD from
Soundgraph's product line "iMON". Soundgraph has not yet provided detailed
information about the compatibility of different Display versions. If you
cannot get "XBMC on iMON Display" to show anything on your LCD or VFD,
please report this so we can help investigate the problem.

## 2. Software Requirements

 - Windows XP or newer
 - Microsoft .NET Framework 2.0: 
   x86: http://www.microsoft.com/downloads/details.aspx?FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5
   x64: http://www.microsoft.com/downloads/details.aspx?familyid=B44A0000-ACF8-4FA1-AFFB-40E78D788B00
 - iMON Manager v7.91.0929: You need to install iMON Manager v7.89.0622 or higher
   (http://download.soundgraph.com/file/setup/iMON_7_90_0702_01.zip), 
   download the update for iMon Manager v7.91.0929 
   (http://download.soundgraph.com/path/patch/Beta/iMON_Patch_7_91_0929_Beta.zip)
   and execute the provided EXE file which updates your iMON Manager.
 - XBMC Dharma Beta 1 or newer: http://mirrors.xbmc.org/releases/win32/


## 3. Installation

There is no real installation. Simply extract all the files into a folder.
If you want to have a desktop icon, right-click the file called XbmcOniMon.exe
and choose "Send to" --> "Desktop". 

## 4. Preparations

Before you start "XBMC on iMON Display" you need to make some configurations.

General
-------
Make sure no other running software on your computer is using TCP port 9090.
You can do that by using NMap (http://nmap.org/) to scan the port and see if
it is currently used. Unfortunately this port is currently hardcoded within
XBMC and therefore can't be changed.

iMON Manager
------------
Open iMON Manager and go to "Options" --> "Plug-in Mode" and activate the "Use 
Display Plug-in Mode" checkbox. Then go to "iMON Utilities" --> "FrontView" and 
make sure "Run FrontView When iMON Starts" is either set to "Always" or 
"Automatically".
Now go to "iMON Utilities" --> "FrontView" and open the "Media Information" tab.
Here you need to disable the "XBMC" option under "Available Player".
Also make sure that iMON Manager is running when you start "XBMC on iMON Display".

XBMC
----
Open XBMC and go into "Settings" --> "Network" --> "Services". Activate the option
"Allow control of XBMC via HTTP" and enter a Port (Username and Password are optional).

## 5. Usage

Start "XBMC on iMON Display" simply by double-clicking the XbmcOniMon.exe.
A window will open up which consists of a menu bar (allowing to manually
connect/disconnect with/from XBMC and initialize/uninitialize the iMON
Display API), a navigation (General, iMON, XBMC) and options. All the options
should be self-explaining. Simply click one of the three option areas in the
navigation to see their specific options. Changing an option immediatelly 
affects the way "XBMC on iMON Display" behaves.

Make sure you provide the correct information to be able to connect with XBMC.

## 6. Known Issues

Here is a list of a few known issues. Please DO NOT report these as bugs!
 - The iMON Display stays dark after initialization when it is deactivated in
   iMON Manager
 - The progress bar does not adjust correctly when Fast Forwarding / Rewinding
 - The display freezes for a short time when pressing a button on a remote. To
   avoid this disable the "iMON Remote Control Messages" in "iMON Manager" --> 
   "iMON Utilities" --> "FrontView" --> "Auto mode"

## 7. Support

If you encounter any bug or have a problem using "XBMC on iMON Display" please visit
the project management site at https://sourceforge.net/apps/trac/xbmc-on-imon/.

## 8. Disclaimer

This Program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2, or (at your option)
any later version.

This Program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with UMM; see the file COPYING.htm in the main solution.  If not, write to
the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.
http://www.gnu.org/copyleft/gpl.html