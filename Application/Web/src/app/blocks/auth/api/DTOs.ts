type AuthData = {
  login: string,
  password: string,
}

type LoginResponse = {
  userId: number,
  username: string,
  token: string,
  expireDate: Date,
}

type CheckLoginRequest = {
  login: string,
}

type CheckLoginResponse = {
  exists: boolean,
}

export type {
  AuthData,
  LoginResponse,
  CheckLoginRequest,
  CheckLoginResponse,
}
