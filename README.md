DiRT: Drop-in Replacement Textures
==================================
DiRT is a fork of Shaw's TextureReplacer, and is used for substituting textures in Kerbal Space Program. The intent of this fork is to simplify the plugin, removing everything except texture replacement.

### What This Plugin Does
* Replaces in-game textures and normal maps.
* Exports a list of the game's textures to a text file.

### What This Plugin Does Not Do
* Anything else.

Specifically, this plugin does not perform Active Texture Management, does not compress textures, does not convert PNG files to RGB, does not generate MipMaps, and does not allow suits and heads to be mapped to specific kerbals. If you need any of those features, please consider using "Texture Replacer" (which is back in active development).

This plugin does not allow replacement of NavBall textures. For NavBall textures, please consider using "Navball Texture Changer".


Instructions
------------
### Installation
The released zip file includes the following folders and files:
	DiRT\ 
	DiRT\GameData\ 
	DiRT\GameData\DiRT\
	DiRT\GameData\DiRT\DiRT.dll
	DiRT\GameData\DiRT\config.cfg
	DiRT\GameData\DiRT\DiRT.version	
	DiRT\GameData\DiRT\Textures\
	DiRT\Source\
	DiRT\Source\DiRT.cs
	DiRT\Source\DiRTActivator.cs
	DiRT\Source\Replacer.cs
	DiRT\Source\Properties\
	DiRT\Source\Properties\AssemblyInfo.cs
	DiRT\README.md

Copy the "DiRT\GameData\DiRT" folder and all of its contents into to the "GameData\" folder of your KSP install.

If you downloaded KSP from the Squad Store or any other direct-download source, this will be the "KSP_win64\GameData\" folder (for Windows), "KSP_osx\GameData\" (for MacOS), or "KSP_linux\GameData\" (for GNU/Linux) created when you installed the game. If you installed the game from Steam, This will be the "steamapps\common\Kerbal Space Program\GameData\" folder in whatever location is used by Steam. You can find this by right clicking on the game's name in your library, selecting Properties, going to the Local Files tab, and selecting "BROWSE LOCAL FILES...".

You do not need the files in the Source folder to use the mod. Those are the C# files needed to recompile DiRT from scratch.

### Replacement Textures
Drop your Replacement Textures into the "GameData\DiRT\Textures\" folder.

### Alternate Configs and Texture Packs
In the folder for your mod or your texture pack, create a "config.cfg" file (or any other name provided the file extension is .cfg) containing a "DiRT_Config" node. Inside that node set a "TextureFolder" entry to the GameData folder where your textures are located. Example followS:

	DiRT_Config
	{
		TextureFolder = MyMod/Textures/
	}
	
### Module Manager Support
DiRT texture paths automatically work with Module Manager. If you wish to include a Module Manager configuration for DiRT in your mod instead of the usual configuration file, use the following template:

	@DiRT_Config
	{
		%TextureFolder = MyMod/Textures/
	}

### Exporting Texture Names
Set the "exportTextureNames" entry in the config file to "True". A file will be created in the main DiRT GameData folder named "GameData/DiRT/ExportedTextureList.txt" with a list of textures and their normal maps. This file will be updated once per game launch.


Common Texture Names
--------------------
### SkyBox
* GalaxyTex_NegativeX
* GalaxyTex_PositiveX
* GalaxyTex_PositiveY
* GalaxyTex_NegativeY
* GalaxyTex_NegativeZ
* GalaxyTex_PositiveZ

### Kerbals
These are listed as: "Texture name (Normal Map) - Short description." 
* kerbalHead (?) - Male kerbal head.
* kerbalGirl_06_BaseColor (?) - Female kerbal head.
* whiteSuite_diffuse (orangeSuite_normal) - The new 1.5 EVA suit texture. This is used for both the helmet and body.
* orangeSuite_diffuse (orangeSuite_normal) - The new 1.5 orange IVA suit. This is used for both the helmet and body.
* paleBlueSuite_diffuse (orangeSuite_normal) - The new 1.5 blue IVA suit. This is used for both the helmet and body.
* EVAjetpack (EVAjetpackNRM) - EVA jetpack texture.
* jetpack (EVAjetpackNRM) - Not sure what this is. A new 1.5.x EVA pack? The in-game model still uses the old texture name (EVAjetpack), just with a different mapping.
* me_suit_difuse_orange (kerbalMainNRM) - Making History suit texture. This is used for both the helmet and body.
* me_suit_difuse_blue (kerbalMainNRM) - Making History suit texture. This is used for both the helmet and body.
* EVAjetpackscondary (EVAjetpacksecondary_N) - Making History  jetpack texture.
* EVALight (?) - Making History helmet light.
* backpack_Diff (backpack_NM) - Parachute.
* edHarris_body (?) - Gene Kerman's body.
* edHarris_head (?) - Gene Kerman's head.
* headSet01 (?) - Gene Kerman's headset.
* wernerVonKerman_body02 (?) - Wernher von Kerman's (second?) body.
* wernerVonKerman_head (?) - Wernher von Kerman's head.
* wernerVonKerman_glasses (?) - Wernher von Kerman's spectacles.


General Comments
----------------
* I strongly suggest the use of DDS/DXT1 or DDS/DXT5 texture files with pre-generated MipMaps.
* Textures will work best if their size is a power of 2. (Ex: 1024x512) If in doubt, use the resolution of the texture you are replacing. 
* Due to the way KSP and/or Unity uses textures, you must rotate the texture 180 degrees and flip horizontally before saving/exporting. 


Change Log
----------
* 1.6.1.0
	- Recompile for KSP v1.6.1
* 1.5.1.0
	- Recompile for KSP v1.5.1
	- Updated this readme file.
* 1.4.3.0
	- Recompile for KSP v1.4.3
	- Removed code which prevented DiRT from running alongside TextureReplacer and TextureReplacerReplaced.	
* 1.4.2.0
	- Recompile for KSP v1.4.2
* 1.4.1.0
	- Recompile for KSP v1.4.1.
	- Fix to prevent missing "_MainTex" and "_BumpMap" log spam.
* 1.4.0.2
	- Fix for normalMap export and logging.
* 1.4.0.1
	- Initial release for KSP v1.4.0. 
* 1.4.0.0
	- Fork from TextureReplacer v2.4.12.


License
-------
Copyright © 2018 Andrew Cummings (Cydonian Monk)

Copyright © 2013-2015 Davorin Učakar, Ryan Bray

Permission is hereby granted, free of charge, to any person obtaining a
copy of this software and associated documentation files (the "Software"),
to deal in the Software without restriction, including without limitation
the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
DEALINGS IN THE SOFTWARE.
