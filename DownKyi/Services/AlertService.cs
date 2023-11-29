﻿using System.Threading.Tasks;
using DownKyi.Images;
using DownKyi.Utils;
using DownKyi.ViewModels.Dialogs;
using Prism.Services.Dialogs;

namespace DownKyi.Services;

public class AlertService
{
    private readonly IDialogService dialogService;

    public AlertService(IDialogService dialogService)
    {
        this.dialogService = dialogService;
    }

    /// <summary>
    /// 显示一个信息弹窗
    /// </summary>
    /// <param name="message"></param>
    /// <param name="buttonNumber"></param>
    /// <returns></returns>
    public async Task<ButtonResult> ShowInfo(string message, int buttonNumber = 2)
    {
        VectorImage image = SystemIcon.Instance().Info;
        string title = DictionaryResource.GetString("Info");
        return await ShowMessage(image, title, message, buttonNumber);
    }

    /// <summary>
    /// 显示一个警告弹窗
    /// </summary>
    /// <param name="message"></param>
    /// <param name="buttonNumber"></param>
    /// <returns></returns>
    public async Task<ButtonResult> ShowWarning(string message, int buttonNumber = 1)
    {
        VectorImage image = SystemIcon.Instance().Warning;
        string title = DictionaryResource.GetString("Warning");
        return await ShowMessage(image, title, message, buttonNumber);
    }

    /// <summary>
    /// 显示一个错误弹窗
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<ButtonResult> ShowError(string message)
    {
        VectorImage image = SystemIcon.Instance().Error;
        string title = DictionaryResource.GetString("Error");
        return await ShowMessage(image, title, message, 1);
    }

    public async Task<ButtonResult> ShowMessage(VectorImage image, string type, string message, int buttonNumber)
    {
        var result = ButtonResult.None;
        if (dialogService == null)
        {
            return result;
        }

        var param = new DialogParameters
        {
            { "image", image },
            { "title", type },
            { "message", message },
            { "button_number", buttonNumber }
        };

        var isEnd = false;
        dialogService.ShowDialog(ViewAlertDialogViewModel.Tag, param,
            buttonResult =>
            {
                result = buttonResult.Result;
                isEnd = true;
            });
        await Task.Run(() =>
        {
            while (true)
            {
                if (isEnd) break;
            }
        });
        return result;
    }
}