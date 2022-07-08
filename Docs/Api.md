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
  "firstName": "test",
  "lastName": "user3000",
  "email": "testUser3000@gmail.com",
  "password": "zaza1234*"
}
```

```js
200 OK
```

#### Register response

```json
{
  "id": "dsfljdsn-skdjfh-sdfsd-sdfsd",
  "firstName": "test",
  "lastName": "user3000",
  "email": "testUser3000@gmail.com",
  "token": "dfsdfdsfsdf"
}
```


### LOGIN
```json
POST {{host}}/auth/login
```

#### Login Request
```json
{
  "email": "testUser3000@gmail.com",
  "password": "zaza1234*"
}
```

```js
200 OK
```

#### Login Response

```json
{
  "id": "dsfljdsn-skdjfh-sdfsd-sdfsd",
  "firstName": "test",
  "lastName": "user3000",
  "email": "testUser3000@gmail.com",
  "token": "dfsdfdsfsdf"
}
```