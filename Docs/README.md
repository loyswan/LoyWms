# LoyWms

## 开发记录

### Domain
在Entities文件夹内  
创建 Entity 类  
继承```: AuditableEntity, IAggregateRoot```

### Application
在Common\Interfaces\Repositories文件夹下  
创建仓库接口接口
继承```IGenericRepositoryAsync<T>```

创建Entity同名的文件夹（复数），用于存放CQRS命令
创建Dto用于查询返回传输对象  
创建Query查询			- 查  
创建Command命令		- 增 改 删  

### Infrastructure
在Persistence项目中 实现 Application 中定义的仓储接口  
在DbContext中，添加`DbSet<Entity>`  
注入Services中，如下  
```services.AddTransient<ICustomerRepositoryAsync, CustomerRepositoryAsync>();```

### WebApi
在Controller文件夹中添加 控制器  
继承自`: ApiControllerBase`

### 更新数据库
在控制台中更新数据库  
add-migration   
如 `add-migration "AddCustomer" -context ApplicationDbContext`  
update-database  
如 `update-database -context ApplicationDbContext`  

