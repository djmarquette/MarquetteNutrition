echo on
c:
cd c:\windows\Microsoft.NET\Framework\v4.0.30319
aspnet_regsql -E -S localhost\sqlexpress -d MNFdb -A mr
pause
