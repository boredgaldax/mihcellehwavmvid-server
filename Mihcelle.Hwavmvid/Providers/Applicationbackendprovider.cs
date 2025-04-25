using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Providers
{
    public class Applicationbackendprovider
    {

        public Applicationcontainer _contextcontainer {  get; set; }
        public List<Applicationcontainercolumn>? _contextcontainercolumns { get; set; }
        public List<Applicationmodulepackage>? _contextframeworkpackages { get; set; }
        public List<Applicationmodulepackage>? _contextpackages { get; set; }
        public Applicationpage _contextpage {  get; set; }
        public Applicationsite _contextsite { get; set; }

    }
}
