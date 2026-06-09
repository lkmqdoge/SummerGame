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
      in 
      {
        default = pkgs.mkShell {
          buildInputs =[ 
            (with pkgs.dotnetCorePackages; combinePackages [
              sdk_10_0
              sdk_9_0
            ])
          ];
          

          DOTNET_ROOT = "${pkgs.dotnet-sdk_9}/share/dotnet";
          LD_LIBRARY_PATH = pkgs.lib.makeLibraryPath [
            pkgs.libGL
            pkgs.libxkbcommon
            pkgs.wayland
          ];
        };
      }
    );
  };
}
