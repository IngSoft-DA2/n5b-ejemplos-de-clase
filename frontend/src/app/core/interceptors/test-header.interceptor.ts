import { HttpInterceptorFn } from '@angular/common/http';

export const testHeaderInterceptor: HttpInterceptorFn = (req, next) => {
    const token = localStorage.getItem("access_token");
    const requestWithTestHeader = req.clone({
        setHeaders: {
        'X-Dummy-Header': 'header-de-prueba',
        'Authorization': token ? `${token}` : ''
        }
    });

  return next(requestWithTestHeader);
};