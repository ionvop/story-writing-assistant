using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace _20231208 {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    // The only purpose for this entire code is to show the error to the user in case the application crashes
    public partial class App : Application {
        // Overrides the OnStartup method to perform additional startup tasks
        protected override void OnStartup(StartupEventArgs e) {
            // Call the base class's OnStartup method
            base.OnStartup(e);

            // Attach event handlers for unhandled exceptions at the application level
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        // Event handler for unhandled exceptions at the application domain level
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            // Convert the exception object to an Exception type
            Exception? exception = e.ExceptionObject as Exception;

            // Display a MessageBox with information about the unhandled exception
            MessageBox.Show($"An unhandled exception occurred: {exception?.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            // Terminate the application with a non-zero exit code (Non-zero means something went wrong)
            Environment.Exit(1);
        }

        // Event handler for unhandled exceptions at the dispatcher (UI) level
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
            // Display a MessageBox with information about the unhandled exception
            MessageBox.Show($"An unhandled exception occurred: {e.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            // Mark the exception as handled to prevent the application from crashing
            e.Handled = true;

            // Close the application anyway
            Environment.Exit(1);
        }
    }
}
