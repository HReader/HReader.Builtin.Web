using System;
using System.IO;
using System.Threading.Tasks;
using HReader.Base;

namespace HReader.Builtin.Web
{
    public class ContentSource : IContentSource
    {
        public string Name => "Web";
        public string Author => "HReader";
        public string Version => "0.0.2-alpha";
        public Uri Website { get; } = new Uri("https://github.com/HReader/HReader.Builtin.Web", UriKind.Absolute);
        
        // The default web content source will handle all http and https scheme uri's
        // if special handling should be necessary for a specific domain use a different
        // scheme and include your own content source looking for that scheme.
        public bool CanHandle(Uri uri)
        {
            return uri.Scheme.Equals("http",  StringComparison.OrdinalIgnoreCase)
                || uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase);
        }

        // this method is called when content is actually requested
        // all data reading is done before consumer returns
        // the stream does not need to linger, it can be disposed before returning
        public async Task HandleAsync(Uri uri, Func<Stream, Task> consumer)
        {
            // simply getting an image over http or https is supported directly by the utilities
            await Utilities.ConsumeImageAsync(uri, consumer);
        }
    }
}
