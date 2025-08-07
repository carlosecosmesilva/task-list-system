FROM node:18-alpine AS build

WORKDIR /app

# Copiar package files
COPY package*.json ./

# Install dependencies (remover --only=production para build)
RUN npm ci

# Copiar código fonte
COPY . .

# Build para produção
RUN npm run build

# Stage de produção
FROM nginx:alpine

# Copiar build
COPY --from=build /app/dist/tasklist-frontend /usr/share/nginx/html

# Copiar configuração do nginx
COPY nginx.conf /etc/nginx/nginx.conf

# Expose port
EXPOSE 80

# Start nginx
CMD ["nginx", "-g", "daemon off;"]