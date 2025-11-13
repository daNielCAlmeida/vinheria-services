pipeline {
    agent any
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        stage('Build') {
            steps {
                echo "Deploy da Vinheria"
            }
        }
        stage('Deploy') {
            steps {
                script {
                    def deployPath = "C:/VinheriaPublicacao"

                    if (!fileExists(deployPath)) {
                        bat "mkdir ${deployPath}"
                    }
                    bat """
                        xcopy /E /I /Y * ${deployPath}
                    """
                }
            }
        }
    }
    post {
        success {
            echo "Deploy finalizado."
        }
        failure {
            echo "Erro no deploy."
        }
    }
}
