# Estructura de carpetas

<aside>
*No hay una estructuración correcta. Suele variar dependiendo, generalmente, de la organización. Acá tienen un ejemplo para seguir….*
</aside>

# Una opción…

```markdown
src/
├── app/
│   ├── core/                   
│   ├── shared/
│   ├── features/
│   │   ├── auth/
│   │   └── profile/
│   │
│   ├── layouts/
│   │   ├── main-layout/
│   │   └── auth-layout/
│   │
│   ├── assets/
│   ├── app-routing.module.ts
│   ├── app.component.ts
│   └── app.module.ts
│
├── assets/
├── styles/
│
├── index.html
├── main.ts
└── angular.json
```

## Responsabilidades

- `core` contiene contenido lógico que se utiliza a lo largo de toda la aplicación. Algunos ejemplos son servicios *(globales… logs, themes….)*, interceptors o guardas (lo vamos a ir viendo…)
- `shared` también contenido reutilizable en toda la aplicación pero muy asociado a la UI. Por ejemplo botones, cards, inputs…
- `features` separamos de forma modular el contenido y lo hacemos específico para cada *feature.* Dentro de cada feature pueden tener algo así:
    
    ```markdown
    ├── components/
    │   ├── login-form/
    │   │   ├── login-form.component.ts
    │   │   ├── login-form.component.html
    │   │   └── login-form.component.scss
    │   └── register-form/
    │       ├── register-form.component.ts
    │       └── ...
    ├── services/
    │   └── auth.service.ts
    ├── pages/
    │   ├── login.page.ts
    │   └── register.page.ts
    └── auth.routes.ts
    ```
    
- `layouts` envuelven a otros componentes y definen cómo se distribuyen en pantalla. Por ejemplo, un *layout* con un *navbar* que mantengo a lo largo de toda la aplicación y voy cambiando el contenido central.