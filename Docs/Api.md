# Dinner Api

- [Dinner Api]
    - [Auth](#auth)
        - [Register](#register)
            - [Register Request](#register-request)
            - [Register Response](#register-response)
        - [Login](#login)
            - [Login Request](#login-request)
            - [Login Response](#login-response)

## Auth

### Register
```js
POST {{host}}/auth/register
```

#### Register request
```json
{
  "firstName": "max",
  "lastName": "awe",
  "email": "bla@bla.com",
  "password": "asdasdasd"
}
```

#### Register response
```json
200 OK
```

```json
{
  "id": "dsfljdsn-skdjfh-sdfsd-sdfsd",
  "firstName": "max",
  "lastName": "awe",
  "email": "bla@bla.com",
  "token": "dfsdfdsfsdf"
}
```


### LOGIN
```js
POST {{host}}/auth/login
```
