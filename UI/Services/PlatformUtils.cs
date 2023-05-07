using CommunityToolkit.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Services;
internal class PlatformUtils
{
    public bool IsMobile()
    {
        return DeviceInfo.Current.Platform == DevicePlatform.Android ||
            DeviceInfo.Current.Platform == DevicePlatform.iOS;
    }

    public bool CanChangeDirectory()
    {
        return !IsMobile();
    }

    public async Task<string?> PickFolder()
    {
        var result = await FolderPicker.Default.PickAsync(CancellationToken.None);
        return result.IsSuccessful ? result.Folder.Path : null;
    }
}
