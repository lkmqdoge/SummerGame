{
  description = "A very basic flake";

  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs?ref=nixos-unstable";
  };

  outputs = { self, nixpkgs }: 
  let 
    inherit (nixpkgs) lib;
    forAllSystems = lib.genAttrs lib.systems.flakeExposed;
  in 
  {
    devShells = forAllSystems (
      system:
      let
        pkgs = nixpkgs.legacyPackages.${system};

        fontsConf = pkgs.makeFontsConf { # fonts for mgcb editor
          fontDirectories = [
            pkgs.nerd-fonts.caskaydia-cove
          ];
        };
      in 
      {
        default = pkgs.mkShell {
          buildInputs =[ 
            (with pkgs.dotnetCorePackages; combinePackages [
              sdk_10_0
              sdk_9_0
            ])
            pkgs.glib                      # gtk things for mgcb
            pkgs.gsettings-desktop-schemas
          ];

          XDG_DATA_DIRS="$XDG_DATA_DIRS:${pkgs.gtk3}/share/gsettings-schemas/${pkgs.gtk3.name}";
          DOTNET_ROOT = "${pkgs.dotnet-sdk_9}/share/dotnet";
          FONTCONFIG_FILE="${fontsConf}";
          LD_LIBRARY_PATH = pkgs.lib.makeLibraryPath [
            pkgs.libGL
            pkgs.libxkbcommon
            pkgs.wayland
            pkgs.gtk3
          ];
        };
      }
    );
  };
}
