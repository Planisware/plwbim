///    This program is free software: you can redistribute it and/or modify
///    it under the terms of the GNU Affero General Public License as published
///    by the Free Software Foundation, either version 3 of the License, or
///    (at your option) any later version.
///
///    This program is distributed in the hope that it will be useful,
///    but WITHOUT ANY WARRANTY; without even the implied warranty of
///    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
///    GNU Affero General Public License for more details.
///
///    You should have received a copy of the GNU Affero General Public License
///    along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.IO;

namespace plwbim {
  /// <summary>
  /// This command line application extracts the hierarchical structure of
  /// the building elements and creates a JSON output compatible with the
  /// xeokit-sdk's metadata model.
  /// See: https://github.com/xeokit/xeokit-sdk/wiki/Viewing-BIM-Models-Offline
  ///
  /// The program takes two arguments, first is the path to the IFC file, the
  /// second one is the path to the output JSON. Currently only IFC 2x3 is
  /// supported.
  ///
  /// Xbim's SDK is used to process the IFC file.
  /// 
  /// The JSON schema can be found in the sources:
  /// https://github.com/bimspot/xeokit-metadata-utils
  /// </summary>
  internal static class Program {
    private static void Main(string[] args) {
      if (args.Length < 2) {
        Console
          .WriteLine("Please specify the path to the IFC and the output json.");
        Console.WriteLine(@"
          Usage:
          
          $ plwbim /path/to/some.ifc /path/to/output.json
        ");
        Environment.Exit(1);
      }

      var ifcPath = args[0];
      if (File.Exists(ifcPath) == false) {
        Console
          .WriteLine("The IFC file does not exists at path: {0}", ifcPath);
        Environment.Exit(1);
      }

      // TODO: create path?
      var jsonPath = args[1];

      try {
        var metaModel = MetaModel.fromIfc(ifcPath);
        metaModel.toJson(jsonPath);
        Environment.Exit(0);
      }
      catch (Exception e) {
        Console.WriteLine(e);
        Environment.Exit(1);
      }
    }
  }
}
