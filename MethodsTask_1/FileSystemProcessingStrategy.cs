using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MethodsTask_1.EventArgs;

namespace MethodsTask_1
{
    public class FileSystemProcessingStrategy : IFileSystemProcessingStrategy
    {
        public ActionType ProcessItemFinded<TItemInfo>(
            TItemInfo itemInfo,
            Func<FileSystemInfo, bool> filter,
            EventHandler<FileFindedEventArgs<TItemInfo>> itemFinded,
            EventHandler<FileFindedEventArgs<TItemInfo>> filteredItemFinded,
            Action<EventHandler<FileFindedEventArgs<TItemInfo>>, FileFindedEventArgs<TItemInfo>> eventEmitter)
            where TItemInfo : FileSystemInfo
        {
            FileFindedEventArgs<TItemInfo> args = new FileFindedEventArgs<TItemInfo>
            {
                FindedItem = itemInfo,
                ActionType = ActionType.ContinueSearch
            };
            eventEmitter(itemFinded, args);

            if (args.ActionType != ActionType.ContinueSearch || filter == null)
            {
                return args.ActionType;
            }

            if (filter(itemInfo))
            {
                args = new FileFindedEventArgs<TItemInfo>
                {
                    FindedItem = itemInfo,
                    ActionType = ActionType.ContinueSearch
                };
                eventEmitter(filteredItemFinded, args);
                return args.ActionType;
            }

            return ActionType.SkipElement;
        }
    }
}
