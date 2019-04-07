sudo apt-get install curl libunwind8 gettext
wget https://download.visualstudio.microsoft.com/download/pr/9650e3a6-0399-4330-a363-1add761127f9/14d80726c16d0e3d36db2ee5c11928e4/dotnet-sdk-2.2.102-linux-arm.tar.gz
wget https://download.visualstudio.microsoft.com/download/pr/9d049226-1f28-4d3d-a4ff-314e56b223c5/f67ab05a3d70b2bff46ff25e2b3acd2a/aspnetcore-runtime-2.2.1-linux-arm.tar.gz

mkdir -p $HOME/dotnet && tar zxf dotnet-sdk-2.2.102-linux-arm.tar.gz -C $HOME/dotnet
export DOTNET_ROOT=$HOME/dotnet 
export PATH=$PATH:$HOME/dotnet

tar zxf aspnetcore-runtime-2.2.1-linux-arm.tar.gz -C $HOME/dotnet
