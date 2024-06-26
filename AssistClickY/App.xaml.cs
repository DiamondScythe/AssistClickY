﻿using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;

using AssistClickY.Contracts.Services;
using AssistClickY.Contracts.Views;
using AssistClickY.Data;
using AssistClickY.Helpers.Clipboard;
using AssistClickY.Helpers.ContextMenu;
using AssistClickY.Helpers.Mouse;
using AssistClickY.Models;
using AssistClickY.Services;
using AssistClickY.ViewModels;
using AssistClickY.Views;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHotkey;
using NHotkey.Wpf;

using Application = System.Windows.Application;

namespace AssistClickY;

// For more information about application lifecycle events see https://docs.microsoft.com/dotnet/framework/wpf/app-development/application-management-overview

// WPF UI elements use language en-US by default.
// If you need to support other cultures make sure you add converters and review dates and numbers in your UI to ensure everything adapts correctly.
// Tracking issue for improving this is https://github.com/dotnet/wpf/issues/1946
public partial class App : Application
{

    public NotifyIcon nfIcon;

    private IHost _host;

    public T GetService<T>()
        where T : class
        => _host.Services.GetService(typeof(T)) as T;

    public App()
    {
    }

    private async void OnStartup(object sender, StartupEventArgs e)
    {
        var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        // For more information about .NET generic host see  https://docs.microsoft.com/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0
        _host = Host.CreateDefaultBuilder(e.Args)
                .ConfigureAppConfiguration(c =>
                {
                    c.SetBasePath(appLocation);
                })
                .ConfigureServices(ConfigureServices)
                .Build();

        await _host.StartAsync();
    }

    private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        // TODO: Register your services, viewmodels and pages here

        // App Host
        services.AddHostedService<ApplicationHostService>();

        // Activation Handlers

        // Core Services

        // Services
        services.AddSingleton<IPageService, PageService>();
        services.AddSingleton<INavigationService, NavigationService>();

        // Views and ViewModels
        services.AddTransient<IShellWindow, ShellWindow>();
        services.AddTransient<ShellViewModel>();

        services.AddTransient<HomeViewModel>();
        services.AddTransient<HomePage>();

        services.AddTransient<SettingsViewModel>();
        services.AddTransient<SettingsPage>();

        // Configuration
        services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));

        //Notification Icon
        nfIcon = new NotifyIcon();
        NotificationIconSetup.SetupNotificationIcon(nfIcon);

        //db
        services.AddDbContext<AssistClickYContext>();

        //hotkeys
        HotkeyManager.Current.AddOrReplace("Increment", Key.M, ModifierKeys.Alt, TestAction);

        //clipboard monitor
        ClipboardMonitor.Start();
        ClipboardMonitor.OnClipboardChange += new ClipboardMonitor.OnClipboardChangeEventHandler(ClipboardMonitor_OnClipboardChange);


    }

    private static void ClipboardMonitor_OnClipboardChange(ClipboardFormat format, object data)
    {
        ClipboardManager.AddNewClipboardItem(format, data);
    }

    // TODO: Change TestAction to a proper function pls
    //hotkeys test
    private void TestAction(object sender, HotkeyEventArgs e)
    {
        MouseRecorder.HotkeyAction();
    }

    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();

        nfIcon.Dispose();

        ClipboardMonitor.Stop();

        _host.Dispose();
        _host = null;
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // TODO: Please log and handle the exception as appropriate to your scenario
        // For more info see https://docs.microsoft.com/dotnet/api/system.windows.application.dispatcherunhandledexception?view=netcore-3.0
    }
}
