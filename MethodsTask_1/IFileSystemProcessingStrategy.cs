using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MethodsTask_1.EventArgs;
namespace MethodsTask_1
{
    interface IFileSystemProcessingStrategy
    {
                ActionType ProcessItemFinded<TItemInfo>(
                TItemInfo itemInfo,
                Func<FileSystemInfo, bool> filter,
                EventHandler<FileFindedEventArgs<TItemInfo>> itemFinded,
                EventHandler<FileFindedEventArgs<TItemInfo>> filteredItemFinded,
                Action<EventHandler<FileFindedEventArgs<TItemInfo>>, FileFindedEventArgs<TItemInfo>> eventEmitter)
                where TItemInfo : FileSystemInfo;
    }
}
