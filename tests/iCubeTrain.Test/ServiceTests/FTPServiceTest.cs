using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iCubeTrain.Services;
using Xunit;

namespace iCubeTrain.Test.ServiceTests
{
    public class FTPServiceTest
    {
        [Fact]
        public async Task TestConnectionAsync_ReturnsTrue_WhenConnectionIsSuccessful()
        {
            // Arrange
            var ftpService = new FTPService();
            var ftpServerUrl = "ftp://localhost";
            var ftpUsername = "user1";
            var ftpPassword = "user1";

            // Act
            var result = await ftpService.TestConnectionAsync(ftpServerUrl, ftpUsername, ftpPassword);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task TestConnectionAsync_ReturnsFalse_WhenConnectionFails()
        {
            // Arrange
            var ftpService = new FTPService();
            var ftpServerUrl = "ftp://invalid.example.com";

            // Act
            var result = await ftpService.TestConnectionAsync(ftpServerUrl);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task TestConnectionAsync_ReturnsFalse_WhenCredentialsAreInvalid()
        {
            // Arrange
            var ftpService = new FTPService();
            var ftpServerUrl = "ftp://localhost";
            var ftpUsername = "invalid";
            var ftpPassword = "invalid";

            // Act
            var result = await ftpService.TestConnectionAsync(ftpServerUrl, ftpUsername, ftpPassword);

            // Assert
            Assert.False(result);
        }
    }
}