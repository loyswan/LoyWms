# LoyWms

## ������¼

### Domain
��Entities�ļ�����  
���� Entity ��  
�̳�```: AuditableEntity, IAggregateRoot```

### Application
��Common\Interfaces\Repositories�ļ�����  
�����ֿ�ӿڽӿ�
�̳�```IGenericRepositoryAsync<T>```

����Entityͬ�����ļ��У������������ڴ��CQRS����
����Dto���ڲ�ѯ���ش������  
����Query��ѯ			- ��  
����Command����		- �� �� ɾ  

### Infrastructure
��Persistence��Ŀ�� ʵ�� Application �ж���Ĳִ��ӿ�  
��DbContext�У����`DbSet<Entity>`  
ע��Services�У�����  
```services.AddTransient<ICustomerRepositoryAsync, CustomerRepositoryAsync>();```

### WebApi
��Controller�ļ�������� ������  
�̳���`: ApiControllerBase`

### �������ݿ�
�ڿ���̨�и������ݿ�  
add-migration   
�� `add-migration "AddCustomer" -context ApplicationDbContext`  
update-database  
�� `update-database -context ApplicationDbContext`  

