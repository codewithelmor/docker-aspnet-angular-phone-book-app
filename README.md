# docker-aspnet-angular-phone-book-app

<a href="https://ibb.co/9G51S15"><img src="https://i.ibb.co/6NCTLTC/2023-11-27-09-49-50.gif" alt="2023-11-27-09-49-50" border="0"></a>

## Docker Commands

```bash
sudo -s
docker rm -vf $(sudo docker ps -aq)
docker rm -f $(sudo docker ps -aq)
docker rmi -f $(sudo docker images -aq)
docker system prune -y

docker-compose build
docker network create app_network
docker images -a
docker ps -a
docker create --network=app_network --name db --hostname db -p 1433:1433 [image_id]
docker start db
docker create --network=app_network --name api --hostname api -p 5057:8080 [image_id]
docker start api
docker create --network=app_network --name client --hostname client -p 4200:80 [image_id]
docker start client

docker exec -it db "bash"
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Password123"
```

```sql
--https://discourse.ubuntu.com/t/microsoft-sql-server-2019-on-ubuntu-20-04/21943
SELECT Name from sys.Databases;
GO
USE PhoneBookApp;
GO

--https://chartio.com/resources/tutorials/sql-server-list-tables-how-to-show-all-tables/
SELECT * FROM INFORMATION_SCHEMA.TABLES;
GO

--https://learn.microsoft.com/en-us/sql/t-sql/functions/serverproperty-transact-sql?view=sql-server-ver16
SELECT SERVERPROPERTY('ServerName') AS ComputerName
GO
```

## xUnit - Unit Test

In **`xUnit`**, the naming convention for test methods is slightly different from other testing frameworks like NUnit or MSTest. xUnit uses a more simplified approach to method naming.

The convention is:

```csharp
public class ClassNameTests
{
    [Fact]
    public void MethodUnderTest_Scenario_ExpectedResult()
    {
        // Arrange

        // Act

        // Assert
    }
}
```

In this convention:

* **`MethodUnderTest`**: This is the name of the method you are testing. Be explicit about what functionality you are testing.

* **`Scenario`**: Describe the scenario or conditions under which the method is being tested. This helps to understand the context of the test.

* **`ExpectedResult`**: Mention the expected outcome of the test. This makes it clear what the expected behavior is.

Here's an example:

```csharp
public class CalculatorTests
{
    [Fact]
    public void CalculateTotal_WithValidInput_CorrectTotal()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.CalculateTotal(5, 10);

        // Assert
        Assert.Equal(15, result);
    }
}
```

In this example:

* **`MethodUnderTest`**: CalculateTotal
* **`Scenario`**: WithValidInput
* **`ExpectedResult`**: CorrectTotal

This naming convention is similar to other frameworks, but it's adapted for xUnit's attribute-based approach. The **`[Fact]`** attribute indicates that the method is a test. The convention helps in making your test suite more readable and maintainable.








