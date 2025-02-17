# Stage 1: Build
FROM node:20-alpine AS build

# Define o diretório de trabalho
WORKDIR /app

# Copia os arquivos package.json e package-lock.json (se existir)
COPY package*.json ./

# Instala as dependências
RUN npm install --legacy-peer-deps

# Copia o restante dos arquivos do projeto
COPY . .

# Executa o build
RUN npm run build

# Stage 2: Produção
FROM nginx:alpine

# Copia os arquivos gerados pelo build para o diretório do Nginx
COPY --from=build /app/dist /usr/share/nginx/html

# Expõe a porta 80
EXPOSE 80

# Inicia o servidor Nginx
CMD ["nginx", "-g", "daemon off;"]