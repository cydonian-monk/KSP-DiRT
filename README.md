DiRT: Drop-in Replacement Textures
==================================
DiRT is a fork of Shaw's TextureReplacer, and is used for substituting textures in Kerbal Space Program. The intent of this fork is to simplify the plugin, removing everything except texture replacement.

### What This Plugin Does
* Replaces in-game textures and normal maps.
* Exports a list of the game's textures to a text file.

### What This Plugin Does Not Do
* Anything else.

Specifically, this plugin does not perform Active Texture Management, does not compress textures, does not convert PNG files to RGB, does not generate MipMaps, and does not allow suits and heads to be mapped to specific kerbals. If you need any of those features, please consider using "Texture Replacer Replaced".

Additionally, this plugin does not allow replacement of NavBall textures. For NavBall textures, please consider using "Navball Texture Changer".


Instructions
------------
### Installation
Copy the provided "DiRT" folder into to the "GameData" folder of your KSP install.

### Replacement Textures
Drop your Replacement Textures into the "GameData/DiRT/Textures" folder.

### Alternate Configs and Texture Packs
In the folder for your pack, create a config.cfg file containing a "DiRT_Config" node. Inside that node set a "TextureFolder" entry to the GameData folder where your textures are located. See the plugin's provided config if you need an example.

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
* Coming Soon.


General Comments
----------------
* I strongly suggest the use of DDS/DXT1 or DDS/DXT5 texture files with pre-generated MipMaps.
* Textures will work best if their size is a power of 2. (Ex: 1024x512) If in doubt, use the resolution of the texture you are replacing. 
* Due to the way KSP and/or Unity uses textures, you must rotate the texture 180 degrees and flip horizontally before saving/exporting. 


Change Log
----------
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
