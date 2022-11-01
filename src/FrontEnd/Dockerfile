FROM nginx:alpine
COPY . /usr/share/nginx/html

docker build -t wp-server-image:v1 .

docker run -d -p 80:80 wp-server-image:v1