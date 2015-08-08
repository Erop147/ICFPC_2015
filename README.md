# ICFPC_2015

SETTING UP (tested on Ubuntu 14.04.2 LTS GNU/Linux 3.13.0-57-generic x86_64)

1. Install Mono 4.0

sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
echo "deb http://download.mono-project.com/repo/debian wheezy main" | sudo tee /etc/apt/sources.list.d/mono-xamarin.list
sudo apt-get update
sudo apt-get install mono-complete

2. Fetch

git clone git@github.com:Erop147/ICFPC_2015.git

3. Build

make

4. Run

./play_icfp2015